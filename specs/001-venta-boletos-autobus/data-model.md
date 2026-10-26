# Modelo de datos: Sistema de venta de boletos de autobús

Este modelo describe las seis entidades de dominio y la agregación diaria indicada en [spec.md](spec.md). Las entidades son datos: declaran campos privados, constructor vacío, constructor con parámetros y propiedades get/set. Cada clase residirá en su archivo. Las decisiones y dependencias técnicas se documentan en [research.md](research.md).

## Entidades

### Usuario

| Campo / propiedad | Tipo conceptual | Reglas |
|---|---|---|
| IdUsuario | entero/identificador | Único dentro de los datos cargados. |
| NombreUsuario | texto | Identifica la cuenta de acceso. |
| Clave | texto | Credencial de acceso; no debe exponerse en mensajes de interfaz o reportes. La especificación no establece política de hash. |
| Rol | enumeración `Administrador` / `Cajero` | Determina las áreas habilitadas. |
| Activo | booleano | Si es falso, el inicio de sesión se rechaza (RN-10). |

**Relación**: un usuario Cajero puede registrar muchos boletos.

### Pasajero

| Campo / propiedad | Tipo conceptual | Reglas |
|---|---|---|
| IdPasajero | entero/identificador | Identificador interno. |
| Cedula | texto | Exactamente 10 dígitos, validación ecuatoriana módulo 10 y única (RN-01). |
| Nombres | texto | Dato mostrado en búsqueda y boleto. |
| Apellidos | texto | Dato mostrado en búsqueda y boleto. |
| FechaNacimiento | fecha | Fuente de cálculo de edad a la fecha de salida/venta; elegible por edad desde los 65 años (RN-02). |
| TieneDiscapacidad | booleano | Si es verdadero, aplica tarifa de 50 % (RN-02). |

**Relación**: un pasajero puede tener muchos boletos. La edad se deriva de la fecha de nacimiento para evitar almacenar dos valores que puedan discrepar.

### Bus

| Campo / propiedad | Tipo conceptual | Reglas |
|---|---|---|
| IdBus | entero/identificador | Identificador interno. |
| NumeroDisco | texto o entero | Identificación operativa del bus; la especificación no define patrón de formato. |
| Placa | texto | Única; patrón `ABC-1234` (RN-08). |
| Capacidad | entero | De 10 a 60, inclusive (RN-08). Define el rango válido de asientos (RN-04). |

**Relación**: un bus puede asignarse a muchos viajes, pero no a dos viajes con la misma fecha y hora (RN-06).

### Ruta

| Campo / propiedad | Tipo conceptual | Reglas |
|---|---|---|
| IdRuta | entero/identificador | Identificador interno. |
| CiudadOrigen | texto | Debe diferir de destino. |
| CiudadDestino | texto | Debe diferir de origen (RN-07). |
| PrecioBase | decimal monetario | Debe ser mayor que cero (RN-07); almacenar con precisión decimal. |
| Duracion | duración | Tiempo estimado del trayecto. La unidad de persistencia debe quedar definida en implementación (por ejemplo minutos). |

**Relación**: una ruta tiene muchos viajes.

### Viaje

| Campo / propiedad | Tipo conceptual | Reglas |
|---|---|---|
| IdViaje | entero/identificador | Identificador interno. |
| IdRuta | referencia a Ruta | Requerida al programar. |
| IdBus | referencia a Bus | Requerida al programar. |
| FechaHoraSalida | fecha y hora | Compuesta para comprobar conflictos de asignación del mismo bus (RN-06) y elegibilidad de venta. |
| Estado | enumeración `Programado`, `Realizado`, `Cancelado` | Solo `Programado`, futuro y con disponibilidad permite venta (RN-05). La hora pasada también debe tratarse como viaje no vendible, aunque el estado permanezca programado. |

**Relaciones**: pertenece a una Ruta y a un Bus; tiene cero o muchos Boletos.

### Boleto

| Campo / propiedad | Tipo conceptual | Reglas |
|---|---|---|
| IdBoleto | entero/identificador | Identificador único de la venta. |
| IdViaje | referencia a Viaje | Requerida. |
| IdPasajero | referencia a Pasajero | Requerida. |
| NumeroAsiento | entero | Entre 1 y la capacidad del bus del viaje, ambos incluidos (RN-04). |
| PrecioPagado | decimal monetario | Precio base completo o 50 % según RN-02; si ambas condiciones aplican, el descuento no se acumula. |
| IdCajero | referencia a Usuario | Usuario autenticado que realizó la venta; debe tener rol Cajero. |
| FechaHoraVenta | fecha y hora | Permite calcular caja diaria y reportes por fecha. |
| Estado | enumeración `Vendido`, `Anulado` | Para cada viaje/asiento solo puede existir como máximo un boleto Vendido; Anulado libera el asiento (RN-03). |
| MotivoAnulacion | texto opcional | Obligatorio no vacío al pasar a Anulado; solo espacios cuenta como vacío (RN-09). |

**Relaciones**: vincula un pasajero y un viaje, y registra al Cajero que la vendió. Cada boleto vendido puede generar su ticket PDF usando `PDFsharp-GDI`; el PDF no introduce una entidad persistida adicional.

## Venta diaria (agregación de consulta)

No es entidad persistida independiente. Se deriva de `Boleto` filtrando por `IdCajero` y fecha calendario actual; el total es la suma de `PrecioPagado` de ventas del cajero en esa fecha para Mi Caja (RF-11). El tratamiento de anulaciones/reembolsos está limitado por la suposición escrita en la especificación. Los reportes generales agrupan las ventas por la ruta asociada al viaje y por la fecha solicitada (RF-12).

## Relaciones resumidas

- Usuario (Cajero) `1:N` Boleto.
- Pasajero `1:N` Boleto.
- Ruta `1:N` Viaje.
- Bus `1:N` Viaje, sujeto a unicidad de bus y fecha/hora de salida.
- Viaje `1:N` Boleto.
- Cada Boleto referencia exactamente un Pasajero, un Viaje y el Cajero que realizó la venta.

## Transiciones

- Usuario: `Activo` puede cambiar a inactivo; un usuario inactivo no autentica.
- Viaje: `Programado` puede permanecer programado, cancelarse o pasar a realizado; viajes cancelados/realizados no admiten nuevas ventas.
- Boleto: `Vendido -> Anulado` únicamente con motivo no vacío. No se describe transición inversa. Al anular, el asiento queda disponible para una venta posterior.

## Matriz RN → validación y pruebas unitarias

| Regla | Validación centralizada del controlador | Prueba MSTest mínima |
|---|---|---|
| RN-01 | Cédula: 10 dígitos, módulo 10 válido y única; rechazo sin alta/modificación en fallo. | Alta válida y casos longitud, caracteres, dígito incorrecto y duplicado. |
| RN-02 | Tarifa es 50 % si edad >=65 o discapacidad; nunca acumular ambos descuentos. | Precio base USD 20: elegible = 10; menor sin discapacidad = 20; elegible por ambos = 10. |
| RN-03 | Venta rechazada si existe boleto no anulado para mismo viaje y asiento. | Rechazo duplicado y disponibilidad tras anulación. |
| RN-04 | Asiento entero en rango [1, capacidad]. | Aceptar límites; rechazar 0, capacidad+1, fracción y entrada no numérica en la frontera de UI. |
| RN-05 | Rechazar viaje realizado, cancelado, pasado o lleno sin cambiar colección; aceptar viaje futuro vigente con lugar. | Casos por estado y lleno, verificando que no se agrega boleto. |
| RN-06 | Rechazar bus ya asignado a la misma fecha/hora exacta. | Conflicto exacto rechazado; otra hora u otra fecha aceptadas. |
| RN-07 | Origen != destino y precio base > 0. | Aceptar ruta válida; rechazar origen=destino y precio <=0. |
| RN-08 | Placa única con patrón `ABC-1234`; capacidad 10..60 inclusive. | Aceptar capacidades límite y placa válida única; rechazar duplicado, formatos erróneos y capacidades fuera de rango. |
| RN-09 | Validar motivo con trim no vacío antes de cambiar estado. | Vacío/espacios no mutan boleto; motivo válido anula y libera asiento. |
| RN-10 | Autenticación requiere cuenta activa además de credenciales correctas. | Activo autentica; inactivo no, incluso con clave correcta. |

## Invariantes de implementación

- Los identificadores usados en relaciones deben resolver una entidad existente en las listas precargadas.
- Las operaciones fallidas deben dejar sin cambios los boletos, disponibilidad de asiento y totales.
- Toda mutación de negocio reside en controladores estáticos; formularios solo recogen entradas, invocan operaciones y presentan sus resultados.
- Los datos precargados y fixtures de pruebas deben ser reproducibles. La limpieza/restauración de listas estáticas solo se expone al ensamblado de pruebas mediante mecanismo interno controlado; no representa una acción de usuario.
