# Churrap

"Es un día feriado, María ansiaba salir al parque a tomar sol, unos mates y, cuando divisase a algún churrero, unos churros; no obstante, está frío y nublado y realmente salir del departamento no parece tan buena opción como hacerse un bollo entre mantas, las ganas de churros persisten, pero no hay nada que hacerles.

Turgut, por otro lado, es el principal sostén económico de su hogar, y cuando otras personas descansan, él comienza el día friendo panificaciones temprano y pedaleando en su bicicleta para venderlas durante la tarde. Desde temprano sabe que va a ser un día difícil, no porque el frío lo desmotive, sino porque sabe que será difícil encontrar gente en los parques a quien venderles sus mercansías.

Así, Turgut sale a pedalear y sin encontrar clientes en las plazas decide volcarse a recorrer las callecitas de los barrios, o quizás las zonas de departamentos en el centro de la ciudad. Así va: tocando su corneta cada par de cuadras, a veces esperando en una esquina, ansiando que alguien le grite a lo lejos _¡churrero!_ y salga a comprarle, pero no tiene mucha suerte.

María, por su parte escucha a lo lejos una corneta y tras vestirse apresuradamente sale a la calle, pero es en vano: no ve por ningún lado a quien pudiera satisfacer su hambre. Espera a escucharla nuevamente, pero el clarín de plástico no vuelve a llenar el aire con su canto de sirena. Grita _¡churrero!_, pero no hay respuesta."

*Churrap* viene a suplir esta desazón producida por el desencuentro de quienes tienen un producto en venta ambulante y no logran encontrar a quienes efectivamente desearían comprarlo, si de hecho pudieran verles.

## Detalles técnicos

Mi plan no es "mostrar todo lo que sé acerca de Xamarin" ni crear una aplicación que sirva de base para alguien que esté buscando aprender, sino tan sólo volver a subirme a una bicicleta que hace años no monto y ver cómo suenan la cantidad de cornetas nuevas que alguien le puso al manubrio sin avisarme. Con suerte terminaré creando una app usable (y que me ahorre salir a buscar churreros con un sonar).

La versión inicial está desarrollada en .Net Core 5.1, constando de un FE (aplicacion de Android) desarrollado en Xamarin, y un backend basado en API REST desplegado en Azure. BD a definir (SQLite para pruebas de momento).

### ¿Qué onda el espanglish?

Lo cotidiano para mí es escirbir código 100% en inglés, no obstante aprovechando que no parece haber mucha divulgación de Xamarin en español (más allá de la doc. oficial de MicroSoft) y siendo que el dominio de este desarrollo es netamente local (¿o existen en paises angloparlantes vendedorxs ambulantes de churros?)

#### ¡¿Es eso lenguaje inclusivo?! ¡¿quiere alguien pensar en lxs niñxs?!

Si, si bien soy un hijo de los 90's al que no le sale con naturalidad, y si bien es cierto que el 90% de quienes venden churros en bici son hombres, no me parece que le haga mal a nadie el flexibilizar un poco la lengua, al fin y al cabo la misma es descriptiva, no prescriptiva, carajo. ... ¿Por qué _churerrxs_ y no _churreres_? No sé, salió así, quizás haga un refactor en el futuro.

## Ejecutando localmente:

1. Para que funcione la app se requiere una API KEY de Google Maps, la misma puede pegarse en el archivo AndroidManifest.xml del proyecto Churrap.Android, o mediante la creacion de un archivho api_keys.xml que la contenga, dentro de la carpeta Resources/values del mismo. Para generar una key propia dirigíte a https://console.cloud.google.com/apis/dashboard crea un proyecto y dentro del mismo una KEY para la API de GoogleMaps. Nota: podrías necesitar crear una cuenta de facturación para esto, pero el uso básico es gratuito.

## TODO:

* Implementar slice vertical del PoC:
    - FE Churrerx: capacidad de cargar posicion actual manualmente
    - FE Cliente: capacidad de ver churrerxs que hayan reportado posicion actual cercana (X metros) recientemente (últimos X minutos)
    - BE: endpoint para cargar posicion actual de churrerx
    - BE: endpoint para devolver churrerxs en actividad cerca
* Implementar autenticación para evitar spam y suplantación de identidad
* Implementar servicio en segundo plano que reporta ubicación continuamente de churrerx
    - FE Churrerx: activa reporte de ubicacion cada X segundos
    - BE: incorpora cálculo de vector de velocidad
    - FE Cliente: muestra dirección / velocidad de los churreros cercanos
* Implementar "corneta":
    - Clientes pueden informar su posicion actual y deseo de comprar
    - Churrerxs pueden informar a quienes están cerca cuando se han detenido en una esquina tocando una corneta
* Implementar botón de "esperame!":
    - Clientes pueden pedirle a un churrerx puntual que se detenga para que le puedan comprar
    - Churrerxs reciben notificacion push

## Changelog (en orden inverso):

* 2022/06/19: agregando detalles a ContactarChurrerx
* 2022/06/19: movida responsabilidad de PosicionActual a nueva BasePosicionViewModel
* 2022/06/18: MapaClientePage ahora usa un AbsoluteLayout asi que el ActivityIndicator no rompe la pagina
* 2022/06/18: sigue el reemplazo por commands y la navegacion a ContactarChurrerxPage
* 2022/06/17: reemplazando Event Handlers por Commands
* 2022/06/15: agregada pagina para contactar churrerxs y logica para mostrarla (WiP)
* 2022/06/12: solución básica para obtener posición actual (al iniciar y manualmente mediante botón)
* 2022/06/07: refactoreada solucion para usar MVVM con AppShell e implementado Modelo y Servicio Mockeado básico
* 2022/06/01: removidas claves hardcodeadas publicamente
* 2022/05/31: implementacion basica de maps
* 2022/05/30: agregado proyecto xamarin esqueleto
* 2022/05/29: git.init