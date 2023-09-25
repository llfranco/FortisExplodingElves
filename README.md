# Exploding Elves

This project implements everything requested in the document sent through email. There is a Demo scene with 4 different spawners, and the spawn rate is customizable through 4 different panels - top left, top right, bottom left and bottom right. These panels also track how many entities are active.

### Instructions

Simply hit play from the Demo scene (located under ``_Project/Content/Scenes``) and it'll auto-start the simulation.

### Architecture

Since the scope is simple, it wasn't necessary to implement super re-usable systems. Overall, the architecture makes use of services and events to handle dependencies between different pieces of the gameplay systems. To avoid creating spaghetti code, lots of places are also using interfaces to abstract interactions between different entities - this helps enforcing the single responsibility pattern.

### Overview

- ``ServiceLocator.cs``: stores and manages all services in the game.

- ``Elf.cs``: implements all the elf character logic - such as movement and collision detection.
    - ``IElf.cs``: abstract version of the ``Elf`` class.
    - ``ElfDefinition.cs``: data container holding all the customizable parameters from the ``Elf`` class - it inherits from ``ScriptableObject``.

- ``ElfSpawner.cs``: implements the spawn logic of elf characters.
  - ``IElfSpawner.cs``: abstract version of the ``ElfSpawner`` class.
  - ``ElfSpawnerSettings.cs``: data container holding all the customizable parameters from the ``ElfSpawner`` class - it inherits from ``ScriptableObject``.

- ``ElfSpawnerSystem.cs``: stores and manages all spawners in the game.
  - ``IElfSpawnerService.cs``: abstract version of the ``ElfSpawnerSystem`` class.

- ``ElfCollisionHandlingSystem.cs``: handles all the elf character collisions by listening to events from ``IElfSpawner`` and ``IElf``.

- ``TeamDefinition.cs``: defines a team to create unique spawners and associate elf characters to those spawners.

- ``ElfSpawnerController.cs``: user interface to display spawner data and customize spawn rate.
