# E-commerce Application

This is a robust e-commerce application built on a microservices architecture, utilizing the CQRS (Command Query Responsibility Segregation) pattern. Developed using .NET 8, this project demonstrates a scalable and maintainable approach to online retail.

## Architecture Overview

- **Microservices**: Each core functionality is encapsulated within its own microservice, allowing for independent development and deployment.
- **CQRS Pattern**: Separate models for reading and writing data optimize performance and scalability.

## Microservices Breakdown

- **Catalog Microservice**: 
  - **Database**: MongoDB
  - Manages product listings, descriptions, and inventory.

- **Basket Microservice**: 
  - **Database**: Redis
  - Handles user shopping carts and session management for quick access.

- **Discount Microservice**: 
  - **Database**: PostgreSQL
  - Manages promotional offers, discounts, and coupon validation.

## Future Development

This project is a work in progress, with ongoing commits planned to enhance features, optimize performance, and improve overall user experience.
