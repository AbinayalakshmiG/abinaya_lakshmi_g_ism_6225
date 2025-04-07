
# Interaction Document

This document summarizes the development process for several assignment questions. For every problem, I recorded prompts asking for guidance on C# syntax for data structures and library methods, as well as for handling errors and coding best practices. The document includes the simulated prompts (focusing on syntax and library usage), a summary of the responses, implementation details, and any adjustments made.

---

## Question 1: Find Missing Numbers in Array

### Prompts Used
- **Prompt 1:**  
  "I am working on an algorithm to find missing numbers in an unsorted array using negative marking. I have a syntax-related doubt regarding the correct way to use a built-in absolute value function when calculating indices in C#. Also, could you show me the proper syntax for using a method that finds the maximum value in an array?"
  
- **Prompt 2:**  
  "I need to raise an error when the maximum value in the array is greater than the array length. What is the recommended syntax for doing this in C#?"


### Response Summary
- "The proper syntax in C# is to use `Math.Abs()` for getting the absolute value and to call `.Max()` on an array or list. Always ensure that any calculated index is within bounds before using it to access an array element. Using try–catch blocks for handling exceptions is recommended for robust code."


---

## Question 4: Find Maximum Product of Three Numbers

### Prompts Used
- **Prompt 1:**  
  "What is the proper C# syntax for sorting an array"
  

### Response Summary
- "The proper syntax in C# is to sort is Array.sort()""
---

### Prompts Used

- **Prompt 1:**  
  "What is the proper way to handle string concatenation in a loop in C#? Should I use a concatenation operator or a more efficient method for this assignment?"


### Response Summary
"The proper syntax in C# is to use `StringBuilder`. However, for simpler string structures concatenation operator is good.

## General Interaction

Below are additional prompts and responses that capture my overall syntax and library method queries during the development process:

*Prompt 1:**  
- I have a doubt, I am solving some problems to get better at programming as a part of my assignment. I am able to solve them, but there are few questions that have only a couple of inputs given. I am unsure about handling the edge cases. In a traditional platform like HackerRank, I would run it and if it breaks, I will handle the exception accordingly. But this is an assignment which I have done in my local environment, so I need to make an assumption on the expected behavior.  
- In the given boilerplate code it looks the exception is caught and thrown again. So is it safe to assume the instructor is expecting me to throw the exception and let it propagate instead of silencing it?"

**Response Summary:**  
- Yes, based on SOLID design principles and coding good practices, known exceptions need to be caught and should be silenced only if it is strictly required. If not, the error should be allowed to propagate to the user or be logged to an external logging system. Since your boilerplate says to throw, a good solution would be to catch the exception, log it, and throw it

**Prompt 2:**  
- "I want to ensure my inline comments are grammatically correct and consolidated at the beginning of each method. Could you help me adjust the tenses and consolidate my comments so that they clearly describe the algorithm and document edge cases?"

**Response Summary:**  
- "Yes, you should consolidate your inline comments at the beginning of each method, ensuring that they are written in correct grammatical form and clearly document the algorithm steps, assumptions, and edge cases. Here is the revised comments"

- **Prompt 3:**  
- "I need to add this interaction history as a file to my repository , making it easier to track changes and for instructors to review. Can you convert it to Markdown, so ?"

**Response Summary:**  
- "Yes, using markdown is an excellent choice because it is well supported by GitHub, making it easy to view, track changes, and review."

---

