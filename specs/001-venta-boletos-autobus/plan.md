# Implementation Plan: Sistema de venta de boletos de autobús

**Branch**: `001-venta-boletos-autobus` | **Date**: 2026-10-25 | **Spec**: [spec.md](spec.md)

**Input**: Feature specification from `/specs/001-venta-boletos-autobus/spec.md`

## Summary

Crear una solución de Visual Studio con una aplicación Windows Forms en C#/.NET Framework 4.8 y un proyecto de pruebas MSTest, organizados según las capas Entidades, Controladores y Formularios. Los controladores estáticos mantendrán listas en memoria precargadas y centralizarán RN-01 a RN-10. La aplicación permitirá administrar la operación, vender/anular boletos, consultar caja/reportes y generar tickets PDF con PDFsharp-GDI bajo licencia MIT.

## Technical Context

**Language/Version**: C# 7.x compatible with .NET Framework 4.8; Windows Forms.

**Primary Dependencies**: MSTest `MSTest.TestFramework` and `MSTest.TestAdapter` (matching pinned version; research verified 4.5.1 on 2026-10-25); `PDFsharp-GDI` 6.2.2 (MIT) for PDF output. Classic .NET Framework projects with `packages.config` and NuGet restore.

**Storage**: In-memory static `List<T>` collections with deterministic seed data; no database or cross-process persistence.

**Testing**: MSTest on .NET Framework 4.8; unit tests for each RN and selected PDF output validation.

**Target Platform**: Windows desktop, Visual Studio, .NET Framework 4.8.

**Project Type**: Two-project Visual Studio solution: WinForms application and MSTest test library.

**Performance Goals**: Meet SC-001 and SC-004 acceptance thresholds.

**Constraints**: UI and messages in Spanish; business rules stay in static controllers; entity classes have private fields, empty and parameterized constructors, and get/set properties; classes reside in separate files. Lists are in-memory and scoped to one process. PDFsharp-GDI is pinned to 6.2.2 under MIT; verify its transitive dependencies during restore/build. CI uses `windows-latest`, NuGet restore, MSBuild, and VSTest; fail if no tests are discovered.

**Scale/Scope**: Two roles, six domain entities, ten business rules (RN-01–RN-10), and seeded sample data; no persistent database.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principle | Gate | Plan assessment |
|---|---|---|
| I. Plataforma Windows y español | WinForms, C#/.NET Framework 4.8, Spanish UI/messages | PASS: fixed in technical context and feature scope. |
| II. Arquitectura en capas | Entidades, Controladores, Formularios; one class per file | PASS: folders and source tree below. |
| III. Entidades uniformes | Private fields, empty/parameterized constructors, get/set properties | PASS: required of each entity; enum types live in separate files. |
| IV. Controladores como autoridad de negocio | Static controllers, static `List<T>`, seeded data, all rules centralized | PASS: controller boundaries and test-state reset are specified in design. |
| V. Formularios sin reglas de negocio | Forms only present UI, collect actions and call controllers | PASS: PDF rendering is a utility; pricing, authorization, and validation stay in controllers. |
| VI. Cobertura unitaria de RN | At least one MSTest for every RN | PASS: data model maps each rule to controller/test coverage; CI treats zero discovered tests as failure. |
| VII. PR and green pipeline | Pull Request and green GitHub Actions pipeline before merge | PASS with operational prerequisite: enable repository branch protection/required checks for `main`; workflow alone cannot enforce merge policy. |

**Pre-design gate**: PASS. No constitutional deviation is proposed. PDFsharp-GDI is selected under its MIT license; verify its pinned package and dependencies during restore/build. No unresolved technical-context field remains.

## Project Structure

### Documentation (this feature)

```text
specs/001-venta-boletos-autobus/
├── plan.md              # This implementation plan
├── research.md          # Phase 0 technical decisions and evidence
├── data-model.md        # Entities, relationships, and RN coverage
├── quickstart.md        # Build, test, and acceptance validation guide
└── tasks.md             # Phase 2 output; not created by this plan
```

No `contracts/` directory is planned: the feature is a local Windows Forms application with no public API or external system interface. The generated PDF is a user-facing document, verified as an output in the data model and quickstart, not an integration contract.

### Source Code (repository root)

```text
.
├── .github/
│   └── workflows/ci.yml
├── SistemaBoletosAutobus.sln
├── SistemaBoletosAutobus.runsettings
├── SistemaBoletosAutobus/
│   ├── Entidades/        # One file per entity and enum
│   ├── Controladores/    # Static controllers, List<T> state, all business rules
│   ├── Formularios/      # Spanish WinForms UI; no business rules
│   ├── Utilidades/       # PDFsharp-GDI PDF rendering only
│   ├── Properties/
│   ├── App.config
│   ├── packages.config
│   ├── SistemaBoletosAutobus.csproj
│   └── Program.cs
└── SistemaBoletosAutobus.Tests/
    ├── Controladores/    # MSTest coverage for RN-01 through RN-10
    ├── Utilidades/       # PDF output checks
    ├── Properties/
    ├── packages.config
    └── SistemaBoletosAutobus.Tests.csproj
```

**Structure Decision**: One classic Visual Studio solution with exactly the two requested .NET Framework 4.8 projects. Entity and controller classes each have individual files. Controller-owned collections remain static and seeded; a test-only internal reset fixture restores known state before each test so static data does not make tests order-dependent. Business logic is not placed in a third project, since no other consumer is specified. CI uses `nuget restore`, `microsoft/setup-msbuild`, MSBuild, and a VSTest path discovered from the installed Visual Studio instance; a `.runsettings` file enables `TreatNoTestsAsError`.

## Post-design Constitution Check

PASS. The design retains all seven constitutional principles. MSTest covers each RN, forms delegate all business decisions, and the two-project structure preserves separation without an unrequested project. The PDF utility only formats a ticket from controller-provided data. Repository branch protection remains a required GitHub setting for Principle VII.

## Complexity Tracking

No constitutional violations or additional architectural projects are proposed.
