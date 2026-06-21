# Mini API project
- MS SQL/.NET 8
- vertical-slice architecture, tests, design patterns

# InvoiceEngine

## 1) What's the business problem?
This mini project belongs to the domain of transport and freight forwarding, specifically invoice management.  
When creating an invoice, the buyer and the seller as contractual parties agree on a **parity (obligation)** — who owes what percentage of the costs.

Examples of parities include:
- EXW → Buyer pays 100% of the goods, Seller 0%.
- FOB → Buyer and Seller share transport costs.
- CIF → Seller covers insurance costs.
- Custom → Buyer and Seller define obligations themselves.

**Note:** In some cases, the buyer and seller can choose a **Custom** option, filling out an additional form to decide who owes what.

**Problem:** This business logic often leads to a large number of `if` conditions, which makes testing and maintenance harder. It is unpredictable what new requirements the client may introduce.

---

## 2) What's the solution to the problem?
The solution is to follow **SOLID principles** and apply the **Strategy pattern** instead of writing endless `if` statements.  
Each parity is implemented as a separate strategy, which allows:

- Easier unit testing for each strategy.
- Simple extension when new parities are introduced.
- Clean and maintainable code.

**Database model:**
- `InvoiceClients` → each client has a role (buyer/seller).
- `RoleFrom` / `RoleTo` → defines who owes whom and in what percentage.
- `Invoice` → connects clients and their obligations.

Testing is done via the `GetById` endpoint, which clearly shows the distribution of obligations within an invoice.

---

## 3) What if the client has new requirements?
Example: the client requests a new parity **JIN**.  
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

