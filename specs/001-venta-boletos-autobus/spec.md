# Feature Specification: Sistema de venta de boletos de autobús

**Feature Branch**: `no asignada`

**Created**: 2026-10-25

**Status**: Draft

**Input**: User description: "Sistema de venta de boletos de autobús para una cooperativa de transporte interprovincial de Ecuador. Usa como fuente los requerimientos y reglas de negocio del archivo docs/01-requerimientos.md (RF-01 a RF-12, RN-01 a RN-10, RNF-01 a RNF-06). Incluye historias de usuario para Administrador y Cajero, con criterios de aceptación medibles para cada regla de negocio."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Administrar la operación de transporte (Priority: P1)

Como Administrador, quiero mantener los usuarios, pasajeros, buses, rutas y viajes de la cooperativa, y consultar reportes de ventas, para que la operación de ventanilla disponga de información vigente y controlada.

**Why this priority**: La configuración de usuarios, vehículos, rutas y horarios hace posible vender boletos para viajes reales; los reportes permiten supervisar las ventas.

**Independent Test**: Con una sesión de Administrador activa, crear y editar los datos requeridos para un viaje, programarlo y consultar ventas para una ruta y fecha determinadas. Verificar también que un usuario Cajero pueda ser creado, editado y desactivado.

**Acceptance Scenarios**:

1. **Given** credenciales correctas de un usuario activo con rol Administrador, **When** inicia sesión, **Then** accede a todas las áreas disponibles; un usuario con credenciales incorrectas o desactivado no inicia sesión.
2. **Given** un Administrador autenticado, **When** crea, edita o desactiva una cuenta, **Then** el cambio queda reflejado y la cuenta desactivada no puede iniciar sesión.
3. **Given** un Administrador autenticado, **When** registra un bus, **Then** el sistema acepta únicamente una placa ecuatoriana de formato `ABC-1234` no registrada y una capacidad de 10 a 60 asientos inclusive.
4. **Given** un Administrador autenticado, **When** registra una ruta, **Then** el origen y destino son ciudades distintas y el precio base es mayor que cero.
5. **Given** un Administrador autenticado, **When** programa un viaje, **Then** queda asociado a una ruta, un bus y una fecha y hora de salida, y el mismo bus no queda asignado a dos viajes en esa misma fecha y hora.
6. **Given** ventas registradas, **When** el Administrador solicita el reporte general, **Then** obtiene las ventas agrupables por ruta y fecha.
7. **Given** un Cajero autenticado, **When** intenta abrir una función de administración, **Then** no puede acceder a ella; el Administrador sí puede acceder a todas las áreas.

---

### User Story 2 - Vender un boleto en ventanilla (Priority: P1)

Como Cajero, quiero localizar o registrar al pasajero, elegir un viaje y un asiento disponible, cobrar el precio que corresponda y emitir el boleto, para completar una venta correcta sin duplicar asientos.

**Why this priority**: La venta de boletos es la operación central de la cooperativa y evita los errores de asignación y cobro que hoy ocurren con el registro en papel.

**Independent Test**: Con un viaje futuro, vigente y con asientos disponibles, iniciar sesión como Cajero, buscar o registrar un pasajero, vender un asiento, comprobar el importe y obtener el ticket PDF. Intentar también las entradas inválidas descritas en los criterios de aceptación de RN-01 a RN-08.

**Acceptance Scenarios**:

1. **Given** credenciales correctas de un Cajero activo, **When** inicia sesión, **Then** solo accede a Punto de Venta, Pasajeros y Mi Caja.
2. **Given** una cédula ecuatoriana válida aún no registrada, **When** el Cajero registra al pasajero, **Then** el pasajero queda disponible para búsqueda por cédula; si la cédula ya existe o no es válida, el registro se rechaza.
3. **Given** un pasajero y un viaje elegibles, **When** el Cajero selecciona un asiento libre dentro de la capacidad del bus y confirma la venta, **Then** se registra una sola venta para ese asiento, se calcula el precio correspondiente y se emite el boleto.
4. **Given** una venta confirmada, **When** el Cajero solicita el ticket, **Then** obtiene un documento PDF correspondiente al boleto vendido.
5. **Given** un pasajero de 65 años o más, o con discapacidad, **When** se calcula el precio, **Then** paga exactamente el 50 % del precio base; si cumple ambas condiciones, el descuento sigue siendo del 50 % y no se acumula.
6. **Given** un viaje ya realizado, cancelado o sin asientos libres, **When** el Cajero intenta vender un boleto, **Then** la venta se rechaza y no se asigna un asiento.
7. **Given** un asiento ya vendido en ese viaje y cuyo boleto no está anulado, **When** otro Cajero intenta vender ese mismo asiento, **Then** la venta se rechaza.
8. **Given** un pasajero encontrado por su cédula, **When** el Cajero edita sus datos y guarda, **Then** la búsqueda devuelve los datos actualizados.
9. **Given** un pasajero registrado, **When** el Cajero confirma su eliminación, **Then** el pasajero deja de aparecer en las búsquedas.

---

### User Story 3 - Anular boletos y revisar la caja (Priority: P2)

Como Cajero, quiero anular un boleto indicando el motivo y consultar el total de mis ventas del día, para mantener el registro de la operación y controlar mi caja.

**Why this priority**: La anulación corrige ventas que no deben mantenerse y la consulta diaria da al Cajero visibilidad de su propia recaudación sin exponer la caja de otros usuarios.

**Independent Test**: Anular un boleto vigente con un motivo, comprobar que el asiento vuelve a estar disponible y verificar que Mi Caja presenta el total del día correspondiente al Cajero autenticado.

**Acceptance Scenarios**:

1. **Given** un boleto vendido y un motivo no vacío, **When** el Cajero confirma la anulación, **Then** el boleto queda anulado y su asiento vuelve a estar libre.
2. **Given** un boleto vendido y un motivo vacío o compuesto solo por espacios, **When** el Cajero intenta anularlo, **Then** la operación se rechaza y el boleto conserva su estado anterior.
3. **Given** ventas de distintos Cajeros y fechas, **When** un Cajero consulta Mi Caja, **Then** ve el total de sus ventas del día actual y no los totales de otros Cajeros.
4. **Given** un boleto anulado, **When** otro Cajero vende el asiento liberado para el mismo viaje, **Then** la nueva venta se permite si el viaje continúa vigente.

### Edge Cases

- Una cédula con longitud distinta de 10 dígitos, caracteres no numéricos, dígito verificador incorrecto o repetida se rechaza sin crear un pasajero.
- La edad exactamente igual a 65 años da derecho al descuento; una edad menor no lo da por edad, salvo que el pasajero tenga discapacidad.
- Los asientos 1 y la capacidad del bus son válidos; el asiento 0 y cualquier número superior a la capacidad se rechazan.
- Un asiento previamente anulado se considera libre; un boleto vendido y no anulado mantiene el asiento ocupado.
- Una hora de salida idéntica para dos viajes del mismo bus en la misma fecha se rechaza; viajes del bus en otra fecha u hora no infringen esta regla.
- Se rechaza una ruta cuando origen y destino son iguales o cuando el precio base es cero o negativo.
- Se rechaza una capacidad de bus menor que 10 o mayor que 60, así como una placa con formato incorrecto o ya existente.
- Un motivo de anulación compuesto únicamente por espacios se trata como vacío.
- Cuando un viaje ya no tiene asientos disponibles, ninguna nueva venta debe modificar ventas, cobros ni disponibilidad.

## Requirements *(mandatory)*

### Functional Requirements

Los identificadores RF, RN y RNF corresponden a `docs/01-requerimientos.md` para conservar trazabilidad con la fuente.

- **RF-01 Autenticación**: El sistema MUST permitir iniciar sesión mediante usuario y clave, y MUST admitir únicamente usuarios activos con credenciales correctas.
- **RF-02 Control por rol**: El sistema MUST restringir al Cajero a Punto de Venta, Pasajeros y Mi Caja; el Administrador MUST poder acceder a todas las funciones.
- **RF-03 Gestión de usuarios**: El Administrador MUST poder crear, editar y desactivar usuarios.
- **RF-04 Gestión de pasajeros**: El sistema MUST permitir registrar, editar, buscar por cédula y eliminar pasajeros.
- **RF-05 Gestión de buses**: El Administrador MUST poder registrar buses con número de disco, placa y capacidad de asientos.
- **RF-06 Gestión de rutas**: El Administrador MUST poder registrar rutas con ciudad de origen, ciudad de destino, precio base y duración.
- **RF-07 Programación de viajes**: El Administrador MUST poder crear viajes asignando una ruta, un bus y una fecha y hora de salida.
- **RF-08 Venta de boleto**: El Cajero MUST poder seleccionar un viaje, pasajero y asiento libre, calcular el precio correspondiente y emitir el boleto.
- **RF-09 Ticket PDF**: El sistema MUST permitir generar en PDF el ticket de un boleto vendido.
- **RF-10 Anulación**: El Cajero MUST poder anular un boleto indicando un motivo; al completar la anulación, el asiento debe quedar disponible.
- **RF-11 Mi Caja**: El sistema MUST permitir al Cajero consultar el total vendido por él durante el día actual.
- **RF-12 Reporte general**: El Administrador MUST poder consultar ventas por ruta y por fecha.

#### Reglas de negocio y criterios de aceptación medibles

- **RN-01 — Cédula ecuatoriana válida y única**: La cédula MUST contener exactamente 10 dígitos, aprobar el algoritmo ecuatoriano de validación módulo 10 y no estar asociada a otro pasajero. **Criterio de aceptación**: En pruebas de registro, se aceptan cédulas únicas que cumplan las tres condiciones; cada cédula inválida, repetida, de longitud distinta de 10 o con caracteres no numéricos es rechazada y no crea ni modifica un pasajero.
- **RN-02 — Descuento**: Los pasajeros de 65 años o más, o con discapacidad, MUST pagar exactamente el 50 % del precio base. **Criterio de aceptación**: Para un precio base de USD 20, el total de cada pasajero elegible es USD 10; una persona menor de 65 años sin discapacidad paga USD 20. Cumplir ambas condiciones no reduce el precio por debajo del 50 % del precio base.
- **RN-03 — Asiento no duplicado**: Un asiento MUST tener como máximo un boleto no anulado por viaje. **Criterio de aceptación**: La segunda venta del mismo asiento en el mismo viaje se rechaza mientras exista un boleto vigente; tras anular el boleto anterior, una nueva venta del asiento se permite.
- **RN-04 — Rango de asientos**: El número de asiento MUST ser un entero entre 1 y la capacidad del bus asignado, ambos límites incluidos. **Criterio de aceptación**: Con capacidad 40, se aceptan los asientos 1 y 40; se rechazan 0, 41, fracciones y valores no numéricos.
- **RN-05 — Viaje vendible**: MUST rechazarse la venta si el viaje ya se realizó, está cancelado o está lleno. **Criterio de aceptación**: Se rechaza la venta en cada uno de esos tres estados y no se registra un boleto ni se ocupa un asiento; se permite en un viaje futuro, no cancelado y con al menos un asiento libre.
- **RN-06 — Disponibilidad del bus**: Un bus MUST no tener dos viajes programados en la misma fecha y hora. **Criterio de aceptación**: Una segunda asignación del mismo bus en la misma fecha y hora se rechaza; una asignación en otra fecha o en otra hora de esa fecha no se rechaza por esta regla.
- **RN-07 — Validez de ruta**: Origen y destino MUST ser diferentes y el precio base MUST ser mayor que cero. **Criterio de aceptación**: Se acepta una ruta con ciudades distintas y precio positivo; se rechaza si las ciudades coinciden o el precio es cero o negativo.
- **RN-08 — Validez del bus**: La placa MUST ser única y cumplir el formato ecuatoriano `ABC-1234`; la capacidad MUST ser de 10 a 60 asientos inclusive. **Criterio de aceptación**: Se acepta una placa única con tres letras, guion y cuatro dígitos, y capacidades 10 y 60; se rechazan placas duplicadas o con formato distinto y capacidades menores que 10 o mayores que 60.
- **RN-09 — Motivo de anulación**: El motivo de anulación MUST ser obligatorio. **Criterio de aceptación**: El sistema rechaza un motivo vacío o compuesto solo por espacios y mantiene el boleto sin cambios; acepta la anulación cuando se proporciona un motivo no vacío.
- **RN-10 — Usuario desactivado**: Un usuario desactivado MUST no poder iniciar sesión. **Criterio de aceptación**: Con credenciales correctas, un usuario activo inicia sesión y uno desactivado no; desactivar una sesión de usuario impide su siguiente intento de acceso.

#### Requisitos no funcionales y restricciones de la fuente

Estos requisitos conservan las restricciones expresas del archivo fuente y son obligatorios para el proyecto.

- **RNF-01 Plataforma**: La solución MUST ser una aplicación de escritorio Windows Forms desarrollada en C# sobre .NET Framework 4.8 con Visual Studio.
- **RNF-02 Organización**: La solución MUST separar responsabilidades en Entidades, Controladores (lógica de negocio) y Formularios.
- **RNF-03 Persistencia**: La información MUST persistirse en memoria mediante listas `List<T>` e incluir datos de prueba precargados.
- **RNF-04 Independencia de la lógica**: La lógica de negocio MUST poder probarse con pruebas unitarias MSTest sin depender de los formularios.
- **RNF-05 Idioma y mensajes**: La interfaz y los mensajes MUST estar en español e indicar claramente errores y confirmaciones.
- **RNF-06 Integración y verificación**: Cada cambio MUST pasar por Pull Request y por un pipeline de GitHub Actions que compile la solución y ejecute las pruebas.

### Key Entities *(include if feature involves data)*

- **Usuario**: Cuenta de acceso, credenciales, rol y estado activo o desactivado.
- **Pasajero**: Persona identificada por cédula, con datos necesarios para la búsqueda, edad y condición de discapacidad.
- **Bus**: Vehículo identificado por número de disco y placa, con capacidad de asientos.
- **Ruta**: Trayecto con ciudad de origen, ciudad de destino, precio base y duración.
- **Viaje**: Salida programada de una ruta en un bus, con fecha, hora y estado.
- **Boleto**: Venta que vincula viaje, pasajero, asiento, precio, Cajero, estado y, cuando corresponda, motivo de anulación.
- **Venta diaria**: Conjunto de ventas asociadas a un Cajero y una fecha para el cálculo de Mi Caja y los reportes.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: En pruebas de aceptación, al menos el 95 % de los Cajeros completa una venta válida y emite el ticket en menos de 2 minutos, desde la selección del viaje hasta la confirmación.
- **SC-002**: El 100 % de los intentos de venta incluidos en las pruebas de límite respeta RN-01 a RN-10, sin duplicar asientos, aplicar descuentos incorrectos ni aceptar datos inválidos.
- **SC-003**: En pruebas funcionales, el 100 % de los intentos de acceso de usuarios desactivados es rechazado y ningún Cajero accede a funciones exclusivas del Administrador.
- **SC-004**: El Administrador puede obtener un reporte filtrado por ruta y fecha en menos de 10 segundos en el conjunto de prueba precargado.
- **SC-005**: Al menos el 90 % de los usuarios de prueba identifica correctamente, sin ayuda, el resultado de una operación a partir de los mensajes en español de confirmación o error.

## Assumptions

- La aplicación se utiliza en las ventanillas de la cooperativa por Administradores y Cajeros autenticados.
- Los viajes, buses y rutas requeridos para una venta ya han sido creados por un Administrador.
- El precio descontado se calcula como la mitad del precio base de la ruta; el descuento no es acumulable.
- Para Mi Caja, “total vendido en el día” se interpreta como el total de las ventas realizadas por el Cajero autenticado en la fecha actual; el tratamiento contable de devoluciones o anulaciones no especificado por la fuente no se extiende en esta especificación.
- Los viajes en memoria y los datos de prueba precargados son el alcance de persistencia de esta versión, conforme a RNF-03.
- Los límites tecnológicos y de flujo de integración enumerados en RNF-01 a RNF-06 son restricciones explícitas de la fuente, aunque no describen resultados de negocio para el usuario final.
