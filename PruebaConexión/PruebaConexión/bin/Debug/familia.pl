existe(X,Y):- dynamic(punto/2), % /2 parametros
         consult('C:\\Users\\ExtremeTech Sc\\source\\repos\\PruebaConexión\\PruebaConexión\\bin\\Debug\\BDPuntos.pl'),
         punto(X,Y).

adyacente([X,Y],[X2,Y2]):- dynamic(punto/2),
			consult('C:\\Users\\ExtremeTech Sc\\source\\repos\\PruebaConexión\\PruebaConexión\\bin\\Debug\\BDPuntos.pl'),
			punto(X,Y), punto(X2,Y2),
			A is X2-1,B is X2+1, C is Y2-1, D is Y2+1,
			((X = X2, Y = D); (X = X2, Y = C);
			(Y = Y2, X = B); (Y = Y2, X = A)).


mismoGrupo2(X,Y,[],_) :- adyacente(X,Y).
mismoGrupo2(X,Y,[Alguno|Lista],Visitados) :- adyacente(X,Alguno),
	not(member(Alguno,Visitados)),
	mismoGrupo2(Alguno,Y,Lista,[Alguno|Visitados]).


final(A,X) :- findall(B,mismoGrupo2(A,B,_,[]),Lista),sort([A|Lista],X).
