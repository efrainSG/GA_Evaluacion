# GA_Evaluacion

La solución está organizada en tres proyectos: **BLService**, **GA_API** y **ConsoleApp1**. Adicionalmente incluye el script de SQL **GA_eserna.sql** que genera la base de datos y sus objetos

## BLService

Contiene:
* Una clase DTO para el envío de información y su recuperación.
* Una interfaz de servicio definida como genérica que especifica un método de guardado y uno de obtención de todos los registros, aunque la única clase que utiliza es *TicketDto*.
* Una implementación de la interfaz donde utiliza procedimientos almacenados para guardar y recuperar información de base de datos
* Una configuración para inyección de dependencias, la cual registra el servicio y su interfaz para que puedan ser inyectados.

## GA_API

Es la API que consume a BLService, el cual se le inyecta como dependencia mediante la interfaz, y expone dos métodos mediante dos verbos:
* **GET** para recuperar todos los registros existentes en la tabla *Ticket*.
* **POST** para guardar en la tabla *Ticket* la información que se le pase mediante el cuerpo de ls solicitud.

## ConsoleApp1

Es una aplicación de consola que cuando se ejecuta, mediante un temporizador realiza el procesamiento de los archivos *FCT* cada minuto. El temporizador trabaja durante poco más de 40 segundos procesando completamente los archivos que pueda durante el tiempo límite y generando archivos de LOG donde registra los archivos conforme se fueron procesando.

La ruta hacia los subdirectorios *Pendientes*, *Procesados* y *Log* se puede especificar como parámetro del ejecutable en la línea de comandos. Si esta ruta no se especifica, se toma por defecto la ubicación del ejecutable.
