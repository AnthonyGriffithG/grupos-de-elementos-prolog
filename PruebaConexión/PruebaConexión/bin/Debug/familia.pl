existe(X,Y):- dynamic(punto/2), % /2 parametros
         consult('C:\\Users\\ExtremeTech Sc\\source\\repos\\PruebaConexión\\PruebaConexión\\bin\\Debug\\BDPuntos.pl'),
         punto(X,Y).

adyacente([X,Y],[X2,Y2]):- dynamic(punto/2),
			consult('C:\\Users\\ExtremeTech Sc\\source\\repos\\PruebaConexión\\PruebaConexión\\bin\\Debug\\BDPuntos.pl'),
			punto(X,Y), punto(X2,Y2),
			A is X2-1,B is X2+1, C is Y2-1, D is Y2+1,
			((X = X2, Y = D); (X = X2, Y = C);
			(Y = Y2, X = B); (Y = Y2, X = A)).


miembroAdy(Punto, [H|T]) :- adyacente(Punto,H) ; miembroAdy(Punto,T).


miembro(N,[N|_]).
miembro(N,[_|Tail]):-
                    miembro(N,Tail).

grupo([]) :- findall([X,Y], punto(X,Y), Lista), grupos(Lista,[]).

