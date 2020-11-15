existe(X,Y):- dynamic(punto/2), % /2 parametros
         consult('BDPuntos.pl'),
         punto(X,Y).

