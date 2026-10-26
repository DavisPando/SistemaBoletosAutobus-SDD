# Fase 1 – Requerimientos del Sistema de Boletos de Autobús

## 1. Contexto del negocio
Una cooperativa de transporte interprovincial de Ecuador vende boletos en ventanilla.
Hoy lo hace en papel, lo que provoca asientos vendidos dos veces, errores en el cobro
de descuentos y falta de control de caja. Se requiere una aplicación de escritorio
para gestionar la venta de boletos.

## 2. Actores
- **Administrador**: gestiona usuarios, buses, rutas, viajes y ve reportes generales.
- **Cajero**: vende y anula boletos, registra pasajeros y consulta su propia caja.

## 3. Requerimientos funcionales
- **RF-01 Autenticación**: el usuario inicia sesión con usuario y clave; solo usuarios activos pueden ingresar.
- **RF-02 Control por rol**: el cajero solo accede a Punto de Venta, Pasajeros y Mi Caja; el administrador accede a todo.
- **RF-03 Gestión de usuarios**: crear, editar y desactivar usuarios (solo administrador).
- **RF-04 Gestión de pasajeros**: registrar, editar, buscar por cédula y eliminar pasajeros.
- **RF-05 Gestión de buses**: registrar buses con número de disco, placa y capacidad de asientos.
- **RF-06 Gestión de rutas**: registrar rutas con ciudad origen, ciudad destino, precio base y duración.
- **RF-07 Programación de viajes**: crear viajes asignando ruta, bus, fecha y hora de salida.
- **RF-08 Venta de boleto**: seleccionar viaje, pasajero y asiento libre; calcular el precio y emitir el boleto.
- **RF-09 Ticket PDF**: generar el ticket del boleto vendido en PDF.
- **RF-10 Anulación**: anular un boleto indicando un motivo; el asiento vuelve a quedar libre.
- **RF-11 Mi Caja**: el cajero ve el total vendido en el día por él.
- **RF-12 Reporte general**: el administrador ve ventas por ruta y por fecha.

## 4. Reglas de negocio
- **RN-01** La cédula del pasajero debe ser ecuatoriana válida (10 dígitos, algoritmo módulo 10) y única.
- **RN-02** Los pasajeros de **65 años o más** o con **discapacidad** pagan el **50 %** del precio base.
- **RN-03** Un asiento no puede venderse dos veces en el mismo viaje (excepto si el boleto anterior está anulado).
- **RN-04** El número de asiento debe estar entre 1 y la capacidad del bus asignado al viaje.
- **RN-05** No se puede vender boletos de un viaje ya realizado, cancelado o lleno.
- **RN-06** Un bus no puede tener dos viajes en la misma fecha y hora.
- **RN-07** La ciudad de origen y destino de una ruta deben ser diferentes; el precio base debe ser mayor que 0.
- **RN-08** La placa del bus debe ser única y con formato ecuatoriano (ABC-1234); la capacidad entre 10 y 60 asientos.
- **RN-09** Para anular un boleto el motivo es obligatorio.
- **RN-10** Un usuario desactivado no puede iniciar sesión.

## 5. Requerimientos no funcionales
- **RNF-01** Aplicación de escritorio Windows Forms en C# (.NET Framework 4.8), Visual Studio.
- **RNF-02** Arquitectura en capas: Entidades, Controladores (lógica de negocio) y Formularios.
- **RNF-03** Persistencia en memoria con listas (List<T>) y datos de prueba precargados.
- **RNF-04** La lógica de negocio no debe depender de los formularios, para poder probarla con pruebas unitarias (MSTest).
- **RNF-05** Interfaz en español, con mensajes claros de error y confirmación.
- **RNF-06** Cada cambio pasa por Pull Request y un pipeline de GitHub Actions que compila y ejecuta las pruebas.
