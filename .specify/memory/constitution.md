<!--
Sync Impact Report
Version change: 1.0.0 -> 1.0.1
Modified principles: none
Added sections: none
Removed sections: none
Follow-up TODOs: none
-->

# Constitución del Sistema de Boletos de Autobús

## Core Principles

### I. Plataforma Windows y español
La aplicación MUST ser Windows Forms, estar desarrollada en C# sobre .NET Framework 4.8 y presentar
su interfaz y mensajes en español. Esta plataforma común mantiene la compatibilidad y una experiencia
coherente para las personas usuarias.

### II. Arquitectura en capas
El código MUST organizarse en las capas Entidades, Controladores y Formularios. Cada clase MUST
residir en su propio archivo. Esta separación hace explícitas las responsabilidades y facilita el
mantenimiento.

### III. Entidades uniformes
Cada entidad MUST declarar campos privados, un constructor vacío, un constructor con parámetros y
propiedades get/set para sus datos. Las entidades representan datos y no sustituyen la lógica de
negocio de los controladores.

### IV. Controladores como autoridad de negocio
Los controladores MUST ser clases estáticas, mantener una `List<T>` estática con datos de prueba
precargados y contener toda la lógica de negocio. Las reglas MUST tener una única ubicación para
evitar comportamientos divergentes entre formularios.

### V. Formularios sin reglas de negocio
Los formularios MUST limitarse a presentar la interfaz, recoger las acciones de la persona usuaria
y llamar a los controladores. MUST NOT implementar reglas de negocio; así las reglas permanecen
centralizadas y pueden probarse sin depender de la interfaz.

### VI. Cobertura unitaria de reglas de negocio
Cada regla de negocio (RN) descrita en `docs/01-requerimientos.md` MUST tener al menos una prueba
unitaria MSTest que verifique su comportamiento. Al agregar o modificar una RN, el cambio MUST
actualizar o añadir su prueba correspondiente.

### VII. Integración protegida por Pull Request y pipeline
Ningún cambio MUST integrarse en `main` fuera de un Pull Request. El pipeline de GitHub Actions
asociado MUST finalizar en verde antes de integrar el cambio. Esto mantiene trazable la revisión y
verifica los controles automatizados antes de actualizar la rama principal.

## Restricciones Técnicas

El desarrollo y las pruebas de la aplicación MUST respetar Windows Forms con C# y .NET Framework
4.8. Las pruebas unitarias de las reglas de negocio MUST utilizar MSTest. Los nombres y textos
visibles para la persona usuaria MUST estar en español.

## Flujo de Desarrollo y Calidad
Cada cambio que afecte una RN MUST incluir pruebas MSTest que cubran la regla afectada. Los Pull
Requests MUST permitir revisar el cumplimiento de esta constitución y MUST tener el pipeline de
GitHub Actions en verde antes de integrar en `main`. La revisión de conformidad MUST comprobar la
separación de capas, la ubicación de la lógica de negocio y la cobertura de las RN afectadas.

## Governance

Esta constitución establece las reglas obligatorias del proyecto. Las enmiendas MUST proponerse
mediante Pull Request y documentar su impacto en principios, estructura y pruebas. El cambio MUST
cumplir las condiciones de integración en `main` establecidas en el Principio VII.

La versión usa SemVer `MAJOR.MINOR.PATCH`: MAJOR para cambios incompatibles, eliminación o
redefinición de principios; MINOR para principios o secciones nuevos y ampliaciones materiales;
PATCH para aclaraciones y cambios editoriales sin alterar obligaciones. Toda revisión MUST comprobar
el cumplimiento de estas reglas y corregir incumplimientos antes de integrar cambios en `main`.

**Version**: 1.0.1 | **Ratified**: 2026-10-25 | **Last Amended**: 2026-10-25
