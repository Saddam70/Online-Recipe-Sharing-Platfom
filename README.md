# Online Recipe Sharing Platform

This repository contains the code for the **Online Recipe Sharing Platform**, a .NET application that allows users to share, rate, and comment on recipes. The platform provides a space where users can interact with each other's culinary creations by sharing recipes, leaving comments, and rating their favorites. The project follows a multi-layer architecture with clear separation of concerns.

## Project Structure

The project is divided into three main layers for better maintainability and scalability:

- **DAL (Data Access Layer)**: 
  - Handles all interactions with the database using **Entity Framework Core**.
  - Contains entity models representing the database schema.
  - Manages CRUD operations for the various entities such as `Recipes`, `Users`, `Comments`, and `Ratings`.
  
- **Service Layer**:
  - Implements the business logic and application rules.
  - Acts as an intermediary between the DAL and the UI layer, processing data before it's presented or saved.
  - Contains services for managing recipes, users, comments, and ratings, ensuring proper validation and business flow.

- **UI Layer**:
  - The frontend of the application, built using **ASP.NET Core MVC**.
  - Manages user interaction and input, providing views to add, edit, and delete recipes, comments, and ratings.
  - Implements responsive design and integrates **Bootstrap** for a user-friendly interface.

## Key Features

- **Recipe Management**:
  - Users can create, update, view, and delete recipes.
  - Recipes are categorized by cuisine and difficulty level, and include cooking time.
  
- **User Authentication**:
  - Users can register, log in, and manage their profiles.
  - Basic JWT authentication has been integrated for API security.

- **Comments and Ratings**:
  - Logged-in users can comment on recipes and provide ratings.
  - Star-based rating system (1-5 stars) to help users discover popular recipes.

- **Search and Filter**:
  - Search functionality to allow users to find recipes by title, cuisine, or other parameters.
  - Filtering options based on cuisine, difficulty, and time.

## Getting Started

To run the project locally:

1. Clone the repository:
   ```bash
   git clone https://github.com/Saddam70/Online-Recipe-Sharing-Platform.git
