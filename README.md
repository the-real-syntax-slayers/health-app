# Homecare Appointment Management Tool

## Overview
This project is a **web-based homecare appointment management tool** developed as part of the **Mandatory Assignment: Basic Project for ITPE3200 Web Applications (Fall 2025)**.  
The system enables healthcare personnel and elderly clients to manage homecare appointments efficiently through a modern web interface built with **.NET Core 8.0 MVC**.

---

## Project Description
The goal of this project is to build a **Minimum Viable Product (MVP)** that demonstrates the core functionality of a homecare scheduling system. The application simplifies communication and coordination between healthcare personnel and older adults receiving homecare.

### Main Features
- **Booking Management:**  
  You can create, view, update, and delete bookings.

---

## Technologies Used
- **Backend:** .NET Core 8.0 (MVC Framework)
- **Frontend:** Razor Views, HTML, CSS, Bootstrap
- **Database:** Entity Framework Core with SQLite
- **Design Pattern:** Repository Pattern and Data Access Layer (DAL)
- **Asynchronous Operations:** Implemented for all database interactions
- **Validation:** Server-side form validation
- **Logging & Error Handling:** Implemented throughout the backend
---

## Setup Instructions

### Prerequisites
- **.NET SDK 8.0**
- **Node.js (version 20.x or newer)**
- **SQLite** (or another supported provider)

### How to Run
1. Clone the project repository.
2. Open the project in **Visual Studio Code** or **Visual Studio**.
3. Run the following command to restore dependencies:
   ```bash
    dotnet restore
    dotnet ef database update
    dotnet run
4. IMPORTANT!! When creating a booking you must use patientId 1 and employeeId 1, because these two already exist in the database.