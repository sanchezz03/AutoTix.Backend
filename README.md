# AutoTix — Railway Ticket Booking System (Microservices)

## 📌 About the Project
AutoTix is a microservices-based booking system designed for searching railway trips, reserving seats, and processing payments in real time.
The system demonstrates modern approaches to building scalable order-processing platforms using event-driven architecture and distributed transactions.

### The project was developed as a master’s qualification work on the topic:

### “Features of Using Microservice Architecture for Building an Order Management System”

## 🎯 Purpose
The main goal of the project is to explore how microservice architecture can be applied to high-load booking systems that integrate with external services.
AutoTix solves typical problems of centralized systems:
* scalability limitations
* tight coupling of components
* difficulty integrating external APIs
* long processing times for complex business operations
   
## 🧩 Key Features
* Search railway trips via official API integration
* Reservation creation and cart management
* Seat and wagon selection
* Payment processing workflow
* Order history tracking
* Asynchronous communication between services
* Fault-tolerant business processes using Saga pattern

## 🏗 Architecture
AutoTix is built using microservices architecture with domain separation:* 
* User Service — authentication and user management
* Order Service — reservation and order lifecycle
* Payment Service — payment processing
* Railway Connector Service — integration with external railway API

## Communication patterns:
* REST API — synchronous operations
* RabbitMQ — asynchronous messaging
* Event-driven workflows
* Saga pattern for distributed transactions

## ⚙️ Technologies Used
* Backend
  * C# / .NET
  * ASP.NET Core Web API
  * MediatR (CQRS)
  * RabbitMQ
  * Redis (caching)
* Frontend
  * React
* Infrastructure
 * Docker (optional)
* RESTful APIs

## 🔗 External Integration
The system integrates with the official railway booking service:
* Ukrainian Railways: https://booking.uz.gov.ua/en/
Integration challenges include rate limiting and anti-bot protection mechanisms.

## 🚀 Why Microservices?
Microservices were chosen to achieve:
* independent scalability of components
* resilience to failures
* parallel processing of user requests
* easier maintenance and extensibility

## 📊 Project Status
This project is a research prototype demonstrating the feasibility of microservices for booking systems and can serve as a foundation for a production-ready solution.
