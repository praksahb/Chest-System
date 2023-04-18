# Chest-System
Chest System for getting rewards similar to clash royale and other games

# Project Goal

- Modular and flexible with aim for scalability.
- Use of design patterns to achieve the above.
- Main aim of project is code quality and architecture

# Project Implementation 

- Chest MVC for instantiating individual chests.
- Instantiated and stored inside list in chest Slot Controller, which is called from Chest Slots Controller.
- Chest Slots Controller is constructed in ChestService which is monobehaviour singleton and at top level of project namespace, so accessible from everywhere.
- ChestService and UIManager are at top level, followed by BaseChest namespace which includes chestMVC and chest states, supplemented by ChestSlot namespace which includes ChestSlotController and ChestSlotsController. Additionally ui elements and UIManager are in namespace ChestSystem.UI  
- Root namespace is ChestSystem same as folder name.
- ChestService and UIManager are the only two scripts which can violate the namespace to allow for easy accessibility and flow of functions.
- Design Patterns used are singletons, state machines and observer pattern(implemented using in-built C# Actions)

# Implementations Left

- Adding assets to the prefabs, which should actually make it look nicer.
- Needs some more refactoring to remove any un-intended namespace violation except for the ones intended in ChestService.
- More refactoring and code review to reduce any unwanted public level methods which can be avoided.
