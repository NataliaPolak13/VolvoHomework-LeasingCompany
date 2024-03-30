# Second Volvo Homework - Leasing Company

This project is a simulation of a leasing company's vehicle fleet management system. This project aims to provide a robust domain model and class hierarchy for managing a fleet of vehicles. The company specializes in leasing both passenger and cargo transport vehicles.

## Project Overview

- **Program.cs**: This file serves as the entry point for the application.

- **Menu.cs**: Responsible for implementing the main menu and handling user interactions. Users can choose various options related to fleet management, such as listing vehicles, calculating fleet value and adding new vehicles.

- **Vehicle.cs**: Defines the base class for all vehicles with common attributes such as color, brand, model, year of manufacture, price, etc.

- **PassengerVehicle.cs** and **CargoTransportVehicle.cs**: These classes represent passenger and cargo transport vehicles, respectively, with specific attributes and methods tailored to each type.

- **VehicleFleet.cs**: Manages the collection of vehicles and provides methods for querying the fleet, such as listing vehicles by brand, calculating total fleet value and determining vehicles requiring maintenance.

- **IVehicleOperations.cs** and **IJsonIO.cs**: Define interfaces for vehicle operations and JSON input/output operations, allowing for extensibility and modularity.

- **JsonIO.cs**: Implements JSON input/output operations, enabling the saving and loading of fleet data to and from JSON files.

## Usage Instructions

1. Upon launching the application, you will be greeted by the main menu.
2. Choose an option from the menu by entering the corresponding number.
3. Follow the instructions and prompts provided by the application.
4. Utilize menu options to list vehicles, calculate fleet value, browse maintenance requirements and more.

## Strengths

- **Object-Oriented Design**: The project follows OOP principles, including classes, inheritance, polymorphism, and encapsulation, ensuring a clear and structured codebase.
- **Modularity and Extensibility**: Interfaces and modular design enable easy extension and modification of functionality, facilitating future enhancements and updates.
- **Comprehensive Functionality**: The application provides a wide range of features for managing a vehicle fleet, including querying, calculation, maintenance tracking and data persistence.
- **Code Quality**: Adherence to established coding conventions and design patterns enhances code readability, maintainability and scalability.

Enjoy managing your leasing company's vehicle fleet with ease and efficiency!

Author Natalia Polak


