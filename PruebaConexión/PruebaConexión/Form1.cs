using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SbsSW.SwiPlCs;
using System.IO;
using System.Collections.ObjectModel;

namespace PruebaConexión
{   
    public partial class Form1 : Form

    {

        DataTable dt = new DataTable();
        private int XY; 
        public List<List<int>> ListaPosiciones = new List<List<int>>();
        public List<List<List<int>>> ListaGrupos = new List<List<List<int>>>();
        public List<String> listaGruposString = new List<String>();
        public string ListaGruposString = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void clickDataTable(object sender, DataGridViewCellEventArgs e)
        {

            if (button3.Enabled)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value == "X")
                {
                    cell.Value = " ";
                    cell.Style.BackColor = Color.White;
                }
                else
                {
                    cell.Value = "X";
                    cell.Style.BackColor = Color.Black;
                }
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Environment.SetEnvironmentVariable("SWI_HOME_DIR", @"C:\\Program Files\\swipl");
            Environment.SetEnvironmentVariable("Path", @"C:\\Program Files\\swipl\\bin");
            string[] p = { "-q", "-f", @"familia.pl" };
            // Connect to Prolog Engine
            PlEngine.Initialize(p);
        }

           
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dt.Rows.Clear();
            dt.Columns.Clear();

            int numero = (int)numericUpDown1.Value;
            XY = numero;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            for (int i = 0; i < numero; i++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < numero; i++)
            {
                dt.Rows.Add();
            }

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowDrop = false;
            dataGridView1.CellClick += clickDataTable;
            listBox1.DoubleClick += new EventHandler(ListBox1_DoubleClick);



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button3.Enabled = false;

            File.Delete("C:\\Users\\ExtremeTech Sc\\source\\repos\\PruebaConexión\\PruebaConexión\\bin\\Debug\\BDPuntos.pl");

            string docPath = "C:\\Users\\ExtremeTech Sc\\source\\repos\\PruebaConexión\\PruebaConexión\\bin\\Debug";

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "BDPuntos.pl"), true))
            {
                outputFile.WriteLine(":- dynamic(punto/2).");
            }

            for (int i = 0; i < XY; i++)
            {
                for (int j = 0; j < XY; j++)
                {
                    dt.Rows[i][j] = " ";
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    

                }
            }
            Random rnd = new Random();
            int cantRandoms = rnd.Next(0, XY*XY+1);
            List<List<int>> noDupes = new List<List<int>>();

            for (int i = 0; i < cantRandoms; i++)
            {
                List<int> posicion = new List<int>();
                int x = rnd.Next(0, XY);
                int y = rnd.Next(0, XY);

                posicion.Add(x);
                posicion.Add(y);

                ListaPosiciones.Add(posicion);
                dt.Rows[x][y] = "X";
                dataGridView1.Rows[x].Cells[y].Style.BackColor = Color.Black;
                dataGridView1.Rows[x].Cells[y].Style.ForeColor = Color.Black;

            }

            for (int i = 0; i < XY; i++)
            {
                for (int j = 0; j < XY; j++)
                {

                    if (dt.Rows[i][j].ToString() == "X")
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "BDPuntos.pl"), true))
                        {
                            outputFile.WriteLine("punto(" + j + "," + i + ").");
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button3.Enabled = false;
            File.Delete("C:\\Users\\ExtremeTech Sc\\source\\repos\\PruebaConexión\\PruebaConexión\\bin\\Debug\\BDPuntos.pl");

            string docPath = "C:\\Users\\ExtremeTech Sc\\source\\repos\\PruebaConexión\\PruebaConexión\\bin\\Debug";

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "BDPuntos.pl"), true))
            {
                outputFile.WriteLine(":- dynamic(punto/2).");
            }

            /*
            PlQuery consulta = new PlQuery("existe(3,2).");
            Console.WriteLine(consulta.SolutionVariables);

            if (consulta.NextSolution() == true)
                Console.WriteLine("fue true");
            else
                Console.WriteLine("fue false");
            */

            for (int i = 0; i < XY; i++)
            {
                for (int j = 0; j < XY; j++)
                {

                    if (dt.Rows[i][j].ToString() == "X") {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "BDPuntos.pl"), true))
                        {
                            outputFile.WriteLine("punto(" + j + "," + i + ").");
                        }
                    }
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ListaGrupos.Clear();
            String general = "";
            for (int i = 0; i < XY; i++)
            {
                for (int j = 0; j < XY; j++)
                {
                    String puntoXY = "[" + j + "," + i + "]";
                    if (dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Black && !general.Contains(puntoXY)) {
                        
                        PlQuery query = new PlQuery("final([" + j + "," + i + "],X).");
                        foreach (PlQueryVariables z in query.SolutionVariables)
                        {
                            List<List<int>> ListaFinalPosiciones = new List<List<int>>();
                            String ListaString = z["X"].ToString();
                            general = general + ListaString;

                            Console.WriteLine(ListaString);
                            int contador = 0;
                            for (int Char = 0; Char < ListaString.Length; Char++)
                            {
                                if (ListaString[Char] == ',') {
                                    contador++;
                                }
                                if ((ListaString[Char] == ',') && (contador % 2 == 0))
                                {
                                    
                                    ListaString = ListaString.Remove(Char, 1);
                                    ListaString = ListaString.Insert(Char, "|");
                                }
                            }
                            
                            ListaString = ListaString.Remove(ListaString.Length - 1, 1);
                            ListaString = ListaString.Remove(0 , 1);

                            String[] posiciones = ListaString.Split('|');
                            String str1 = string.Join("|", posiciones);
                            str1 = str1.Replace("]", "");
                            str1 = str1.Replace("[", "");

                            String[] pares = str1.Split('|');
                            
                            foreach (string subpar in pares)
                            {

                                String[] nums = subpar.Split(',');
                                List<int> subposicion = new List<int>();
                                Console.WriteLine(subpar);
                                subposicion.Add(int.Parse(nums[0]));
                                subposicion.Add(int.Parse(nums[1]));
                                int y = int.Parse(nums[0]);
                                //subposicion.Add((int)Char.GetNumericValue(subpar[0]));
                                //subposicion.Add((int)Char.GetNumericValue(subpar[2]));


                                ListaFinalPosiciones.Add(subposicion);
                            }

                            ListaGrupos.Add(ListaFinalPosiciones);
                            listaGruposString.Add(ListaString);
                            listBox1.Items.Add(ListaString);
                        }

                        
                        
                        query.NextSolution();
                        query.Dispose();
                    }
                    
                    
                }
                

            }


            var l1 = new List<int>() {};

            foreach (var i in ListaGrupos) {
                l1.Add(i.Count());
            }
            l1.Sort();
            var g = l1.GroupBy(i => i);
            string mensaje = "Cantidad de grupos: " + ListaGrupos.Count() + "\n";
            foreach (var grp in g)
            {
                mensaje = mensaje + "Con tamaño " + grp.Key + " hay " + grp.Count() + " grupos\n";
            }
            MessageBox.Show(mensaje);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < XY; i++)
            {
                for (int j = 0; j < XY; j++)
                {
                    dt.Rows[i][j] = " ";
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.White;

                }
            }

            button1.Enabled = true;
            button3.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            

            List<Color> colores = new List<Color>();
            for (int a = 0; a < ListaGrupos.Count; a++)
            {
                
                Color miColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                colores.Add(miColor);

                for (int b = 0; b < ListaGrupos[a].Count; b++)
                {

                    dataGridView1.Rows[ListaGrupos[a][b][1]].Cells[ListaGrupos[a][b][0]].Style.BackColor = colores[a];
                    dataGridView1.Rows[ListaGrupos[a][b][1]].Cells[ListaGrupos[a][b][0]].Style.ForeColor = colores[a];

                }
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < XY; i++)
            {
                for (int j = 0; j < XY; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Selected) {

                        x = j;
                        y = i;
                        break;
                    }

                }
            }
            Random r = new Random();
            Color miColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            Console.WriteLine("X: " + x + " Y:" + y);
            int tam = 0;
            for (int a = 0; a < ListaGrupos.Count; a++)
            {
                for (int b = 0; b < ListaGrupos[a].Count; b++)
                {
                    
                    if (ListaGrupos[a][b][1] == y && ListaGrupos[a][b][0] == x) {
                        tam = ListaGrupos[a].Count();
                        for (int B = 0; B < ListaGrupos[a].Count; B++)
                        {
                            dataGridView1.Rows[ListaGrupos[a][B][1]].Cells[ListaGrupos[a][B][0]].Style.BackColor = miColor;
                            dataGridView1.Rows[ListaGrupos[a][B][1]].Cells[ListaGrupos[a][B][0]].Style.ForeColor = miColor;
                        }
                    }
                }
            }
            MessageBox.Show("El grupo seleccionado tiene " + tam + " elementos.");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < XY; i++)
            {
                for (int j = 0; j < XY; j++)
                {
                    if (dt.Rows[i][j].ToString() == "X") {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                        dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                    }
                }
            }
            if (listBox1.SelectedItem != null)
            {
                int indice = listBox1.SelectedIndex;
                MessageBox.Show(indice.ToString());
                foreach (var i in ListaGrupos[indice]) {
                    dataGridView1.Rows[i[1]].Cells[i[0]].Style.BackColor = Color.Cyan;
                    dataGridView1.Rows[i[1]].Cells[i[0]].Style.ForeColor = Color.Cyan;
                }
            }
        }
    }
}
