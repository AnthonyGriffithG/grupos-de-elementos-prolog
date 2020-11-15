femenino(petra).
femenino(carmen).
femenino(maria).
femenino(rosa).
femenino(ana).
femenino(cande).
madre(petra, juan).
madre(petra, rosa).
madre(carmen, pedro).
madre(maria, ana).
madre(maria, enrique).
madre(rosa, raul).
madre(rosa, alfonso).
madre(rosa, cande).
masculino(angel).
masculino(pepe).
masculino(juan).
masculino(pedro).
masculino(enrique).
masculino(raul).
masculino(alfonso).
padre(angel, juan).
padre(angel, rosa).
padre(pepe, pedro).
padre(juan, ana).
padre(juan, enrique).
padre(pedro, raul).
padre(pedro, alfonso).
padre(pedro, cande).

progenitor(X, Y) :- padre(X, Y).
progenitor(X, Y) :- madre(X, Y).

abuelo(X, Y) :- padre(X, Z), progenitor(Z, Y).
abuela(X, Y) :- madre(X, Z), progenitor(Z, Y).

hermana(X,Y) :- femenino(X), progenitor(Z,X), progenitor(Z,Y), X \= Y.

feliz(X) :- (padre(X,Y) ; madre(X,Y)), masculino(Y), !.

tienedosniños(X) :- progenitor(X,Y), masculino(Y), hermana(_,Y),!.

nieto(X) :- progenitor(Y,X), progenitor(_,Y).

tia(X,Y) :- femenino(X), progenitor(Z,Y), hermana(X,Z).

miembro(N,[N|_]).
miembro(N,[_|Tail]):- miembro(N,Tail),!.

eliminar(_,[],[]).
eliminar(H,[H|T],R) :- eliminar(H,T,R).
eliminar(E,[H|T],[H|R]) :- eliminar(E,T,R).

eliminarRep([],[]).
eliminarRep([H|T],R) :- miembro(H,T), eliminarRep(T,R).
eliminarRep([H|T],[H|R]) :- eliminarRep(T,R).


