# Mini API project
- MS SQL/.NET 8
- vertical-slice architecture, tests, design patterns

# Invoice Engine

## 1) What's the business problem?
This mini project belongs to the domain of transport and freight forwarding, specifically invoice management.  
When creating an invoice, the buyer and the seller as contractual parties agree on a **incoterm (obligation)** — who owes what percentage of the costs.

Incoterm obligation rules table:
<img width="1382" height="307" alt="image" src="https://github.com/user-attachments/assets/defb45f7-1a93-4a2f-ac45-6cde0728127f" />

**Note:** In some cases, the buyer and seller can choose a **Custom** option, filling out an additional form to decide who owes what.

**Problem:** This business logic often leads to a large number of `if` conditions, which makes testing and maintenance harder. It is unpredictable what new requirements the client may introduce.

---

## 2) What's the solution to the problem?
The solution is to follow **SOLID principles** and apply the **Strategy pattern** instead of writing endless `if` statements.  
Each incoterm is implemented as a separate strategy, which allows:

- Easier unit testing for each strategy.
- Simple extension when new incoterms are introduced.
- Clean and maintainable code.

**Database model:**
<img width="1140" height="758" alt="image" src="https://github.com/user-attachments/assets/e94a73a1-59ec-4895-8341-f8bdff09fdec" />

There are two tables that can tell... 
- InvoiceClients table, the logic is that one client can have one role (Seller or Buyer).
- InvoiceItemObligations => tells us who is paying amount

---

## 3) What if the client has new requirements?
Example: the client requests a new incoterm **JIN**.  
- Buyer has the obligation to pay X% of the debt.  
- Seller covers the remaining costs.

**Solution:** Add a new strategy to the Strategy pattern.  
**Advantage:** No need to modify existing code or add new `if` conditions. The system remains extensible and testable.

---

## 4) Conclusion
- There are multiple ways to solve this problem:
  - **Database tables + user function** to calculate obligations.  
    - Pros: centralized logic.  
    - Cons: additional queries, harder to test.
  - **Strategy pattern in code**.  
    - Pros: easier testing, clean code, extensible.  
    - Cons: requires more classes, but ensures maintainability.

- The code is **easily extensible** and follows **SOLID principles**.  
- When the client introduces new requirements, the problem is solved by adding a new strategy without breaking the existing system.  
- This project demonstrates understanding of the **financial domain (obligations)** and the application of **enterprise practices** (CQRS, Strategy, integration testing with Testcontainers).

