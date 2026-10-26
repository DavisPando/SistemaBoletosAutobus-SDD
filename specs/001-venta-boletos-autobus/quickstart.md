# Guía rápida de validación

Esta guía define comprobaciones ejecutables para validar el sistema completo. Las reglas, entidades y transiciones están en [data-model.md](data-model.md); las decisiones de versiones y herramientas están en [research.md](research.md).

## Prerrequisitos

- Windows con Visual Studio y workload de desarrollo de escritorio .NET instalado.
- .NET Framework 4.8 Developer Pack/Targeting Pack.
- NuGet CLI, MSBuild y Visual Studio Test Platform (VSTest).
- Acceso a NuGet para restaurar `packages.config`, incluidos MSTest y PDFsharp-GDI.
- La solución `SistemaBoletosAutobus.sln` y los dos proyectos .NET Framework 4.8 ya implementados.

## Restaurar, compilar y probar localmente

Desde la raíz del repositorio, en PowerShell:

```powershell
nuget restore .\SistemaBoletosAutobus.sln -NonInteractive
msbuild .\SistemaBoletosAutobus.sln /m /p:Configuration=Release
vstest.console.exe .\SistemaBoletosAutobus.Tests\bin\Release\SistemaBoletosAutobus.Tests.dll /Settings:.\SistemaBoletosAutobus.runsettings /Logger:trx /ResultsDirectory:.\TestResults
```

Si `vstest.console.exe` no está en `PATH`, localizarlo con `vswhere` dentro de la instalación de Visual Studio/Test Platform y ejecutar la ruta encontrada; no asumir que la acción de configuración de MSBuild también configura VSTest. El archivo runsettings debe hacer que una ejecución con cero pruebas descubiertas falle. Resultado esperado: NuGet restaura sin errores, MSBuild compila ambos proyectos en Release, VSTest descubre y pasa las pruebas MSTest, y produce TRX en `TestResults`.

## Validaciones funcionales

1. **Administración y autenticación**: cargar los datos de ejemplo, iniciar sesión con Administrador activo y comprobar sus áreas disponibles. Repetir con Cajero y verificar que una cuenta inactiva/credenciales incorrectas no inicia sesión y el rol Cajero no abre funciones administrativas.
2. **Pasajero y tarifa**: registrar una cédula ecuatoriana válida única y buscarla; rechazar cédula inválida y repetida sin crear/modificar datos. Con precio base USD 20, comprobar USD 10 para edad 65 o discapacidad, USD 20 para persona no elegible y USD 10 cuando ambas condiciones aplican.
3. **Bus, ruta y programación**: comprobar placas válidas/únicas y capacidades en límites 10 y 60; rechazar valores fuera de rango. Crear ruta con ciudades distintas y precio positivo. Rechazar otra programación del mismo bus a la misma fecha/hora y permitir otra fecha u hora.
4. **Venta y asiento**: vender asiento 1 y el asiento igual a capacidad en un viaje futuro con disponibilidad. Rechazar asiento 0, mayor a capacidad, asiento vigente duplicado y viaje realizado, pasado, cancelado o lleno; tras cada rechazo comprobar que no cambió la lista de boletos ni disponibilidad.
5. **PDF**: generar ticket para boleto vendido en un flujo de guardado temporal de prueba. Verificar que el archivo existe, no es vacío, tiene cabecera PDF (`%PDF-`) y contiene visualmente/digitalmente los campos mínimos definidos por la implementación, al menos identificador del boleto, pasajero, ruta/viaje, asiento y precio. Borrar el archivo temporal al finalizar el test. Una venta rechazada no debe producir ticket.
6. **Anulación y caja/reportes**: rechazar motivo vacío o de espacios conservando boleto; anular con motivo, comprobar que el asiento vuelve a estar disponible y que puede revenderse si el viaje sigue vigente. Comparar Mi Caja para Cajeros distintos y fechas distintas; comprobar que reporte general se filtra/agrupa por ruta y fecha.

Los casos que verifican reglas de negocio deben estar automatizados en MSTest, cubriendo individualmente RN-01 a RN-10 conforme a la matriz en [data-model.md](data-model.md). Las verificaciones de tiempo usan el conjunto precargado: el flujo válido debe completarse bajo 2 minutos para SC-001 y el reporte bajo 10 segundos para SC-004.

## Validar el pipeline de GitHub Actions

- Abrir Pull Request hacia `main` y comprobar que el workflow Windows ejecuta restauración NuGet, compilación MSBuild Release y pruebas VSTest.
- Confirmar que el trabajo falla ante restauración, compilación, prueba fallida o cero pruebas descubiertas.
- Revisar y publicar el resultado TRX como artefacto/result summary para depuración.
- Verificar que branch protection exige el check verde antes de permitir merge; la configuración del workflow por sí sola no impone la política de rama.

Resultado esperado: todos los tests pasan en Windows, el resumen incluye pruebas descubiertas y no hay errores de build; el pull request queda sujeto a la política de protección de `main`.
