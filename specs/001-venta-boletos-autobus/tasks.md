# Tasks: Sistema de venta de boletos de autobús

**Input**: Design documents from `/specs/001-venta-boletos-autobus/`

**Prerequisites**: plan.md (required), spec.md (required), research.md, data-model.md, quickstart.md

**Organization**: Tasks are grouped by user story to enable independent implementation and verification of each story.

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Initialize the Windows Forms solution and test project infrastructure.

- [X] T001 Create the Visual Studio solution folders and repository structure in `SistemaBoletosAutobus/`, `SistemaBoletosAutobus.Tests/`, and `docs/`.
- [X] T002 Initialize the .NET Framework 4.8 WinForms project and MSTest project configuration in `SistemaBoletosAutobus/SistemaBoletosAutobus.csproj`, `SistemaBoletosAutobus.Tests/SistemaBoletosAutobus.Tests.csproj`, and `SistemaBoletosAutobus/packages.config`.
- [X] T003 [P] Configure NuGet dependencies, CI workflow, and test runner settings in `SistemaBoletosAutobus/packages.config`, `SistemaBoletosAutobus.Tests/packages.config`, `.github/workflows/ci.yml`, and `SistemaBoletosAutobus.runsettings`.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Add the shared domain model, static controllers, and ticket utilities that all stories depend on.

**⚠️ CRITICAL**: No user story work can begin until this phase is complete.

- [X] T004 Create the entity classes and enums for `Usuario`, `Pasajero`, `Bus`, `Ruta`, `Viaje`, and `Boleto` in `SistemaBoletosAutobus/Entidades/`.
- [X] T005 Implement the shared in-memory seed data and static controller foundations for authentication, validation, ticketing, and reporting in `SistemaBoletosAutobus/Controladores/`.
- [X] T006 [P] Add the PDF ticket utility and shared Spanish validation/error formatting in `SistemaBoletosAutobus/Utilidades/` and `SistemaBoletosAutobus/Formularios/`.

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel.

---

## Phase 3: User Story 1 - Administrar la operación de transporte (Priority: P1) 🎯 MVP

**Goal**: Allow the Administrator to manage accounts, buses, routes, trips, and sales reports with role-based access controls.

**Independent Test**: Start an Administrator session, create and edit the required records, program a trip, and validate the route/date reporting flow without using the Cajero screens.

### Tests for User Story 1

- [ ] T007 [P] [US1] Create MSTest coverage for authentication, user activation, bus validation, route validation, trip scheduling, and general reports in `SistemaBoletosAutobus.Tests/Controladores/`.

### Implementation for User Story 1

- [ ] T008 [US1] Implement authentication, user CRUD, role restriction, and deactivation logic in `SistemaBoletosAutobus/Controladores/UsuarioController.cs`.
- [ ] T009 [P] [US1] Implement bus registration and validation logic for a unique Ecuadorian plate `ABC-1234` and capacity between 10 and 60 seats inclusive in `SistemaBoletosAutobus/Controladores/BusController.cs`.
- [ ] T010 [P] [US1] Implement route validation and trip scheduling logic for distinct origin/destination, base price > 0, and bus/date-time conflict rejection in `SistemaBoletosAutobus/Controladores/RutaController.cs` and `SistemaBoletosAutobus/Controladores/ViajeController.cs`.
- [ ] T011 [US1] Implement general sales reporting aggregated by route and date in `SistemaBoletosAutobus/Controladores/ReporteController.cs`.
- [ ] T012 [US1] Build the Administrator forms for login, user management, bus management, route management, trip scheduling, and reporting views in `SistemaBoletosAutobus/Formularios/`.

**Checkpoint**: At this point, User Story 1 should be fully functional and independently testable.

---

## Phase 4: User Story 2 - Vender un boleto en ventanilla (Priority: P1)

**Goal**: Let a Cajero register or find a passenger, validate the trip/seat, calculate the correct price, and issue a ticket PDF while rejecting invalid sales.

**Independent Test**: Open a valid future trip with available seats, sell a ticket as a Cajero, confirm the total and PDF output, and then exercise the invalid sale states described by RN-01 through RN-08.

### Tests for User Story 2

- [ ] T013 [P] [US2] Create MSTest coverage for Ecuadorian ID validation, passenger CRUD, pricing discounts, seat duplication rejection, and PDF generation in `SistemaBoletosAutobus.Tests/Controladores/` and `SistemaBoletosAutobus.Tests/Utilidades/`.

### Implementation for User Story 2

- [ ] T014 [US2] Implement passenger creation, lookup by cédula, editing, and deletion rules in `SistemaBoletosAutobus/Controladores/PasajeroController.cs`.
- [ ] T015 [P] [US2] Implement sale validation and discount logic for passenger age >= 65 or disability with exact 50% pricing and seat conflict prevention in `SistemaBoletosAutobus/Controladores/BoletoController.cs`.
- [ ] T016 [P] [US2] Implement PDF ticket generation and the cashier sale flow in `SistemaBoletosAutobus/Utilidades/TicketPdf.cs` and `SistemaBoletosAutobus/Formularios/`.
- [ ] T017 [US2] Build the Punto de Venta, Pasajeros, and ticket output screens for the cashier workflow in `SistemaBoletosAutobus/Formularios/`.

**Checkpoint**: At this point, User Story 2 should work independently of the admin workflow.

---

## Phase 5: User Story 3 - Anular boletos y revisar la caja (Priority: P2)

**Goal**: Allow a Cajero to cancel a sale with a non-empty reason and review only their daily sales totals.

**Independent Test**: Cancel a sold ticket with a valid reason, verify the seat becomes available, and confirm that the Cajero sees only their current-day totals and not another cashier's totals.

### Tests for User Story 3

- [ ] T018 [P] [US3] Create MSTest coverage for cancellation reasons, seat release after cancellation, and daily cash totals by cashier in `SistemaBoletosAutobus.Tests/Controladores/`.

### Implementation for User Story 3

- [ ] T019 [US3] Implement boleto cancellation, reason validation, and seat release logic in `SistemaBoletosAutobus/Controladores/BoletoController.cs`.
- [ ] T020 [US3] Implement daily cash totaling and cashier-scoped totals in `SistemaBoletosAutobus/Controladores/CajaController.cs`.
- [ ] T021 [US3] Build the cancellation and Mi Caja interfaces in `SistemaBoletosAutobus/Formularios/`.

**Checkpoint**: At this point, the day-end and refund/cancellation workflow is independently testable.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Final validation, usability, and operational hardening across the entire solution.

- [ ] T022 [P] Run the acceptance-validation flow from `quickstart.md` and confirm the full WinForms workflow, login constraints, business-rule scenarios, and PDF generation all pass in `SistemaBoletosAutobus/` and `SistemaBoletosAutobus.Tests/`.
- [ ] T023 [P] Clean up code quality, Spanish messaging, and documentation in `README.md`, `docs/`, and the feature files under `specs/001-venta-boletos-autobus/`.
- [ ] T024 Finalize CI verification, ensure zero-test discovery is treated as a failure, and confirm all reports and performance thresholds are satisfied in `.github/workflows/ci.yml`, `SistemaBoletosAutobus.runsettings`, and the test output directory.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately.
- **Foundational (Phase 2)**: Depends on Setup completion; blocks all user stories.
- **User Story 1 (Phase 3)**: Depends on Foundational completion.
- **User Story 2 (Phase 4)**: Depends on Foundational completion and can run in parallel with User Story 1 if team capacity allows.
- **User Story 3 (Phase 5)**: Depends on Foundational completion and can run in parallel with User Story 2 after the core controller base is stable.
- **Polish (Phase 6)**: Depends on all desired user stories being complete.

### User Story Dependencies

- **US1 (P1)**: Must finish before the full admin/reporting workflow is considered release-ready.
- **US2 (P1)**: Can proceed in parallel with US1 after the foundational rules and seed data are stable.
- **US3 (P2)**: Can proceed in parallel with US2 after the ticket and seat validation rules are complete.

### Parallel Opportunities

- All Setup tasks marked `[P]` can run in parallel.
- The shared PDF and validation utility work in Phase 2 can run alongside the static controller foundation.
- The admin test matrix and bus/route/trip implementation in US1 can be split across different developers.
- Passenger validation and sale pricing logic in US2 can be implemented independently from the ticket UI flow.
- Cancellation logic and the Caja totals logic in US3 can be developed concurrently once the ticket model is defined.

---

## Parallel Example: User Story 2

```bash
# Run in parallel during the sale workflow implementation:
Task: "Create MSTest coverage for Ecuadorian ID validation, passenger CRUD, pricing discounts, seat duplication rejection, and PDF generation in SistemaBoletosAutobus.Tests/Controladores/ and SistemaBoletosAutobus.Tests/Utilidades/"
Task: "Implement passenger creation, lookup by cédula, editing, and deletion rules in SistemaBoletosAutobus/Controladores/PasajeroController.cs"
Task: "Implement sale validation and discount logic in SistemaBoletosAutobus/Controladores/BoletoController.cs"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup.
2. Complete Phase 2: Foundational.
3. Complete Phase 3: User Story 1.
4. Validate the Administrator workflow independently.
5. Stop and confirm the report and business rules are green before expanding to cashier operations.

### Incremental Delivery

1. Establish the shared model and controller foundation.
2. Deliver US1 for admin configuration and reporting.
3. Deliver US2 for the cashier sale flow and document generation.
4. Deliver US3 for cancellation and daily cashier totals.
5. Finish with polish, quickstart validation, and CI hardening.

### Parallel Team Strategy

With multiple developers:

1. One developer completes Setup and Foundation.
2. Developer A: User Story 1 (admin + reporting).
3. Developer B: User Story 2 (sale + ticketing).
4. Developer C: User Story 3 (cancel + caja).
5. Final pass by the team for quickstart validation and CI verification.

---

## Notes

- [P] tasks = different files, no dependencies on incomplete tasks.
- [USx] labels map tasks to the referenced user story for traceability.
- Each story is independently testable with a focused validation scenario.
- Validation and acceptance checks remain aligned with the RN matrix in `specs/001-venta-boletos-autobus/data-model.md`.
- Stop after each story checkpoint to validate the workflow before starting the next layer.
