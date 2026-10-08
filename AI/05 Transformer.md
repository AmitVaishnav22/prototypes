# The Transformer Architecture

How the Transformer architecture lets LLMs understand relationships between words.

## Mind map

```mermaid
mindmap
  root((Transformer))
    The problem
      Language has hidden relationships
      Who does he refer to
      The boy went to the shop because he needed milk
    Attention mechanism
      Vital component of the Transformer
      Analyzes surrounding words
      Calculates word-to-word relationships
      Finds which words are most relevant
    Three terms clarified
      Transformer
        The underlying architecture
        The engine of modern AI models
      LLM
        The final trained AI product
        Built using the Transformer design
      Attention
        One component inside the Transformer
    RAG vs Transformer
      RAG
        Retrieves relevant data
        From external sources like a vector database
      Transformer
        Processes the retrieved context
        Understands relationships
        Generates coherent response
    Looking ahead
      Query Key Value calculations
      The mechanics inside Attention
```

## How the pieces relate

```mermaid
flowchart TD
    A["Transformer<br/>The underlying architecture"] --> B["Attention<br/>One component inside the Transformer"]
    A --> C["Other components<br/>Alongside Attention"]
    A --> D["LLM<br/>The final trained AI product"]
```

## RAG and Transformer working together

```mermaid
flowchart LR
    A["User query"] --> B["RAG<br/>Retrieves relevant context from a vector database"]
    B --> C["Retrieved context + query"]
    C --> D["Transformer<br/>Processes context to understand relationships"]
    D --> E["Coherent response"]
```

## Attention resolving a pronoun

```mermaid
flowchart LR
    S["The boy went to the shop because he needed milk"] --> Q["Pronoun: he"]
    Q -->|"high relevance"| W1["boy"]
    Q -.->|"low relevance"| W2["shop"]
    Q -.->|"low relevance"| W3["milk"]
```

## Summary

### The problem with language

LLMs must work out how words in a sentence relate to each other. In "The boy went to the shop because he needed milk," the model needs to figure out that "he" refers to "the boy," not to anything else in the sentence.

### The Attention mechanism

Attention is a vital component of the Transformer design. It lets the model look at the surrounding words in a sentence, calculate how they relate to one another, and determine which words are most relevant to a specific pronoun or subject.

### Transformer vs LLM vs Attention

These three terms are often used loosely, but they mean different things:

- **Transformer**: the underlying architectural design, the "engine" of modern AI models.
- **LLM**: the final trained AI product, built using the Transformer design.
- **Attention**: one of several essential components inside the Transformer architecture.

### RAG vs Transformer

RAG and the Transformer solve different parts of the problem:

- **RAG** focuses on finding and retrieving relevant data from external sources, like a vector database, to provide context.
- **Transformer** focuses on processing that retrieved context to understand language relationships and generate a coherent response.

### Looking ahead

A deeper dive into Query, Key, and Value (Q, K, V) calculations, the technical mechanics used within the Attention mechanism, is a natural next step.

## Key takeaways

| Concept | In one line |
| --- | --- |
| Language relationship problem | LLMs must resolve what words like "he" refer to in a sentence |
| Attention | Calculates relevance between words to resolve these relationships |
| Transformer | The underlying architecture or "engine" behind modern AI models |
| LLM | The trained AI product built using the Transformer architecture |
| RAG | Retrieves relevant external context, such as from a vector database |
| Transformer's role with RAG | Processes retrieved context into a coherent response |
| Looking ahead | Query, Key, and Value (Q, K, V) calculations inside Attention |