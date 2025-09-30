# 📚 Library Management System (LMS)

## 1. Overview

The **Library Management System (LMS)** is designed to manage books, categories, authors, members, loans, and fines in an efficient and structured way.

### Features

* Organize books by categories and authors.
* Register and track members easily.
* Borrowing and returning automation.
* Fine calculation for overdue or damaged items.

### System Behavior

* Members can search books by **title, author, or category**.
* When borrowing, the system assigns a **due date** (default: 14 days).
* If returned on time → **no fine**.
* If overdue or damaged → a fine is **calculated automatically**.
* If fines remain unpaid → the member’s account may be set to **Suspended**.

---

## 2. Entities & Their Details

### 2.1 Book

**Attributes:**

* `Id`
* `Title`
* `Price`
* `PublicationYear`
* `AvailableCopies`
* `TotalCopies`

**Relationships:**

* One **Book → One Author**
* One **Book → One Category**
* One **Book → Many Loans**

---

### 2.2 Category

**Attributes:**

* `Id`
* `Title`
* `Description`

**Relationships:**

* One **Category → Many Books**

---

### 2.3 Author

**Attributes:**

* `Id`
* `FirstName`
* `LastName`
* `DateOfBirth`

**Relationships:**

* One **Author → Many Books**

---

### 2.4 Member

**Attributes:**

* `Id`
* `Name`
* `Email`
* `PhoneNumber`
* `Address`
* `MembershipDate`
* `Status (Active, Suspended)`

**Relationships:**

* One **Member → Many Loans**

---

### 2.5 Loan

**Attributes:**

* `Id`
* `LoanDate`
* `Status (Borrowed, Returned, Overdue)`
* `DueDate`
* `ReturnDate`

**Relationships:**

* One **Loan → One Member**
* One **Loan → One Book**
* One **Loan → One Fine (if overdue/damaged)**

---

### 2.6 Fine

**Attributes:**

* `Id`
* `Amount`
* `IssuedDate`
* `PaidDate`
* `Status (Pending, Paid)`

**Relationships:**

* One **Fine → One Loan**

---

## 3. Class Diagram

<img width="900" height="565" alt="image" src="https://github.com/user-attachments/assets/0631bd88-0226-4de1-bdd4-cc8b2f0cdb5e" />

---

## 4. Model Configurations

### 4.1 Book

* `Title` → `varchar(50)`
* `Price` → `decimal(6,2)`
* `PublicationYear` → Between 1950 and current year
* `AvailableCopies` ≤ `TotalCopies`

### 4.2 Category

* `Title` → `varchar(50)`
* `Description` → `varchar(100)`

### 4.3 Author

* `FirstName` → `varchar(20)`
* `LastName` → `varchar(20)`

### 4.4 Member

* `Name` → `varchar(50)`
* `Email` → `varchar(100)` with valid email format
* `PhoneNumber` → `varchar(11)` (valid Egyptian phone number)
* `Address` → `varchar(100)`
* `MembershipDate` → Default: insertion date
* `Status` → Stored as label

### 4.5 Loan

* `LoanDate` → Default: insertion date
* `Status` → Stored as label

### 4.6 Fine

* `Amount` → `decimal(6,2)`
* `IssuedDate` → Default: insertion date
* `Status` → Stored as label

---

## 5. ER Diagram
<img width="900" height="565" alt="image" src="https://github.com/user-attachments/assets/0631bd88-0226-4de1-bdd4-cc8b2f0cdb5e" />


---

## 6. Database Schema

*(Include schema script or diagram)*

---

## 7. Database Diagram

*(Insert diagram here if available)*

---

## 8. Data Seed

* Authors
* Categories
* Books
* Members

---

## 9. Data Manipulation Queries

1. Retrieve the book title, its category title, and the author’s full name for all books whose price is greater than 300.
2. Retrieve all authors and their books (if any exist).
3. Member with `Id = 1` borrows the book with `Id = 2` and will return it after 5 days.
4. After 10 days, Member with `Id = 1` returns the book.
5. Retrieve all members who currently have **active loans** (loans not yet returned).

---

## 🚀 Getting Started

* Clone the repository.
* Apply migrations:

  ```bash
  dotnet ef database update
  ```
* Run the project:

  ```bash
  dotnet run
  ```
