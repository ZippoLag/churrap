# Churrap

"Es un día feriado, María ansiaba salir al parque a tomar sol, unos mates y, cuando divisase a algún churrero, unos churros; no obstante, está frío y nublado y realmente salir del departamento no parece tan buena opción como hacerse un bollo entre mantas, las ganas de churros persisten, pero no hay nada que hacerles.

Turgut, por otro lado, es el principal sostén económico de su hogar, y cuando otras personas descansan, él comienza el día friendo panificaciones temprano y pedaleando en su bicicleta para venderlas durante la tarde. Desde temprano sabe que va a ser un día difícil, no porque el frío lo desmotive, sino porque sabe que será difícil encontrar gente en los parques a quien venderles sus mercansías.

Así, Turgut sale a pedalear y sin encontrar clientes en las plazas decide volcarse a recorrer las callecitas de los barrios, o quizás las zonas de departamentos en el centro de la ciudad. Así va: tocando su corneta cada par de cuadras, a veces esperando en una esquina, ansiando que alguien le grite a lo lejos _¡churrero!_ y salga a comprarle, pero no tiene mucha suerte.

María, por su parte escucha a lo lejos una corneta y tras vestirse apresuradamente sale a la calle, pero es en vano: no ve por ningún lado a quien pudiera satisfacer su hambre. Espera a escucharla nuevamente, pero el clarín de plástico no vuelve a llenar el aire con su canto de sirena. Grita _¡churrero!_, pero no hay respuesta."

*Churrap* viene a suplir esta desazón producida por el desencuentro de quienes tienen un producto en venta ambulante y no logran encontrar a quienes efectivamente desearían comprarlo, si de hecho pudieran verles.

## Detalles técnicos

La versión inicial está desarrollada en .Net Core 5.1, constando de un FE (aplicacion de Android) desarrollado en Xamarin, y un backend basado en API REST desplegado en Azure. BD a definir (SQLite para pruebas de momento).

## TODO:

. Implementar slice vertical del PoC:
    - FE Churrerx: capacidad de cargar posicion actual manualmente
    - FE Cliente: capacidad de ver churrerxs que hayan reportado posicion actual cercana (X metros) recientemente (últimos X minutos)
    - BE: endpoint para cargar posicion actual de churrerx
    - BE: endpoint para devolver churrerxs en actividad cerca
. Implementar autenticación para evitar spam y suplantación de identidad
. Implementar servicio en segundo plano que reporta ubicación continuamente de churrerx
    - FE Churrerx: activa reporte de ubicacion cada X segundos
    - BE: incorpora cálculo de vector de velocidad
    - FE Cliente: muestra dirección / velocidad de los churreros cercanos
. Implementar "corneta":
    - Clientes pueden informar su posicion actual y deseo de comprar
    - Churrerxs pueden informar a quienes están cerca cuando se han detenido en una esquina tocando una corneta
. Implementar botón de "esperame!":
    - Clientes pueden pedirle a un churrerx puntual que se detenga para que le puedan comprar
    - Churrerxs reciben notificacion push

## Changelog (en orden inverso):

. 2022/05/29: git.init