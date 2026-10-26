# Investigación técnica: Sistema de venta de boletos de autobús

**Fecha de consulta**: 2026-10-25

## Plataforma y solución

**Decision**: Mantener C# Windows Forms en .NET Framework 4.8 en la aplicación y el proyecto de pruebas. La solución contendrá exactamente los dos proyectos definidos por el usuario: `SistemaBoletosAutobus` y `SistemaBoletosAutobus.Tests`. Usar proyectos clásicos de .NET Framework con `packages.config` y restauración de NuGet.

**Rationale**: La plataforma y la separación son requisitos explícitos de RNF-01 a RNF-04 y de la constitución. El formato clásico es natural para un proyecto WinForms de .NET Framework y compatible con el uso de MSBuild/NuGet restore en Windows.

**Alternatives considered**: Migrar a .NET moderno, usar SDK-style projects o extraer un tercer proyecto de dominio. No se adoptan porque contradicen o exceden el alcance tecnológico pedido.

**Sources**: [Versiones y dependencias de .NET Framework](https://learn.microsoft.com/en-us/dotnet/framework/install/versions-and-dependencies); [NuGet package references](https://learn.microsoft.com/en-us/nuget/consume-packages/package-references); [NuGet package restore](https://learn.microsoft.com/en-us/nuget/consume-packages/package-restore).

## MSTest

**Decision**: Usar `MSTest.TestFramework` y `MSTest.TestAdapter` fijados en la misma versión estable, con destino .NET Framework 4.8. La versión 4.5.1 fue observada en NuGet durante esta consulta; confirmar la pareja y compatibilidad al crear los proyectos y probar la restauración en CI.

**Rationale**: Framework y adapter cumplen responsabilidades complementarias (API de pruebas y descubrimiento/ejecución); fijar ambas versiones evita discrepancias no deterministas. Las pruebas cubren cada RN sin inicializar formularios. Las listas estáticas necesitan fixtures que dejen el estado de prueba controlado entre casos.

**Alternatives considered**: Probar lógica a través de la interfaz o utilizar otro framework. Se descartan por la independencia exigida y la obligación de MSTest en la constitución.

**Sources**: [MSTest.TestFramework](https://www.nuget.org/packages/MSTest.TestFramework); [MSTest.TestAdapter](https://www.nuget.org/packages/MSTest.TestAdapter); [NuGet package restore](https://learn.microsoft.com/en-us/nuget/consume-packages/package-restore).

## PDFsharp

**Decision**: Usar `PDFsharp-GDI` 6.2.2 en `Utilidades` para generar el PDF del ticket, con versión fijada en `packages.config`. El paquete declara licencia MIT y soporte para .NET Framework 4.6.2, por lo que el target .NET Framework 4.8 satisface el requisito de framework. Limitar esta capa a maquetar los datos del boleto que recibe del controlador; no duplicar validaciones ni reglas de negocio.

**Rationale**: Se elige conforme a la solicitud de usar PDFsharp bajo licencia MIT y se utiliza la variante GDI destinada a aplicaciones Windows Forms. El paquete `PDFsharp` multiplataforma 6.2.2, en contraste, apunta a .NET Standard 2.0/.NET 6/.NET 8 y declara dependencias de `Microsoft.Extensions.Logging` y `System.Security.Cryptography.Pkcs`; para WinForms .NET Framework, la variante GDI declara explícitamente compatibilidad con .NET Framework 4.6.2.

**Alternatives considered**: iTextSharp 5, descartado por solicitud explícita y su licencia distinta; `PDFsharp` multiplataforma, descartado por preferir el paquete GDI de Windows Forms con target .NET Framework declarado. MigraDoc no es necesario para la plantilla sencilla de un ticket y no se añade como dependencia sin requisito.

**Sources**: [PDFsharp-GDI 6.2.2 NuGet metadata](https://api.nuget.org/v3-flatcontainer/pdfsharp-gdi/6.2.2/pdfsharp-gdi.nuspec); [PDFsharp 6.2.2 NuGet metadata](https://api.nuget.org/v3-flatcontainer/pdfsharp/6.2.2/pdfsharp.nuspec); [Microsoft .NET Standard compatibility](https://learn.microsoft.com/en-us/dotnet/standard/net-standard); [PDFsharp documentation](https://docs.pdfsharp.net/).

## GitHub Actions y pruebas en CI

**Decision**: Ejecutar el pipeline en `windows-latest`. Pasos explícitos: restaurar solución con NuGet, compilar Release con MSBuild y ejecutar el ensamblado de pruebas con VSTest. Localizar VSTest mediante herramientas presentes en Visual Studio Runner (por ejemplo, `vswhere`) o una acción de configuración que se valide en el workflow; no asumir que `microsoft/setup-msbuild` lo agrega al `PATH`. Configurar `.runsettings` con `TreatNoTestsAsError` para impedir una ejecución exitosa sin pruebas descubiertas. Publicar resultados TRX.

**Rationale**: Los proyectos de .NET Framework y Windows Forms requieren un runner Windows con MSBuild, framework y Visual Studio Test Platform. Separar pasos vuelve explícita la restauración, compilación y ejecución. Un fallo por cero pruebas evita que una regresión en descubrimiento de MSTest pase inadvertida.

**Alternatives considered**: Ejecutar el workflow en runner Linux, invocar `vstest.console.exe` asumiendo una ruta fija, o confiar únicamente en que el proceso devolvió código cero. Se descartan por requisitos de plataforma y fiabilidad del descubrimiento.

**Sources**: [GitHub runner images](https://github.com/actions/runner-images); [GitHub-hosted runners](https://docs.github.com/en/actions/concepts/runners/github-hosted-runners); [setup-msbuild](https://github.com/microsoft/setup-msbuild); [VSTest console options](https://learn.microsoft.com/en-us/visualstudio/test/vstest-console-options?view=vs-2022); [runsettings](https://learn.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2022); [NuGet restore CLI](https://learn.microsoft.com/en-us/nuget/reference/cli-reference/cli-ref-restore).

## Decisiones de alcance y riesgos técnicos

**Decision**: Mantener datos en memoria según RNF-03; no incorporar base de datos. Los controladores estáticos serán la única autoridad para la validación y transición de venta/anulación. Usar colecciones precargadas deterministas y preparar el estado de pruebas con fixtures dedicados.

**Rationale**: Es el alcance expreso de la especificación y permite probar reglas sin formularios ni servicios externos.

**Alternatives considered**: Persistencia en archivo o base de datos y arquitectura de repositorios; ambas amplían el alcance actual y no son necesarias para cumplir los requisitos fuente.

**Riesgo**: Los datos se pierden al cerrar el proceso y la memoria no coordina instancias diferentes. Esto es una limitación deliberada de esta versión y no debe presentarse como persistencia durable.

**Decisión de ticket**: El PDF es un documento generado a partir de una venta ya confirmada; validar extensión MIME/estructura PDF y presencia de identificador y datos del boleto en pruebas, además de verificar que no se emita como efecto colateral de una venta rechazada. La especificación no solicita QR, código de barras, firma digital ni esquema visual detallado.
