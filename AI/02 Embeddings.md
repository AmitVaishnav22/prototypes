# Embeddings: Giving Tokens Meaning

How AI models go from meaningless token IDs to a mathematical understanding of word meaning.

## Mind map

```mermaid
mindmap
  root((Embeddings))
    The problem
      Token IDs are just roll numbers
      No inherent meaning
      Can't tell related vs unrelated concepts
    The solution
      Embeddings
        Tokens become vectors
        Lists of floating-point numbers
      Meaning maps
        Similar words sit close together
        dog and puppy are near
        airplane is far away
    The math
      Dot products
        Measure distance between vectors
        Semantic similarity, not keyword match
        refund matches reimbursement
    The origin
      Learned during training
        Not assigned by humans
        Learned from vast amounts of text
    What comes next
      Semantic search
      Vector databases
      RAG
```

## From token ID to meaning

```mermaid
flowchart TD
    A["Token<br/>e.g. 'dog'"] --> B["Token ID<br/>A meaningless roll number"]
    B --> C["Embedding<br/>Learned vector of floating-point numbers"]
    C --> D["Position in meaning space<br/>Similar words placed close together"]
    D --> E["Dot product comparison<br/>Measures semantic similarity between vectors"]
    E --> F["Model grasps meaning<br/>refund ~ reimbursement, dog ~ puppy"]
```

## Meaning space: near vs far

```mermaid
flowchart LR
    subgraph Close["Close together: similar meaning"]
        dog(("dog"))
        puppy(("puppy"))
    end
    subgraph Far["Placed far apart: unrelated"]
        airplane(("airplane"))
    end
    dog <-. "small distance" .-> puppy
    dog -. "large distance" .-> airplane
```

## Summary

### Why token IDs aren't enough

Text is converted into tokens, and each token gets a unique ID, like a roll number. On their own, these IDs carry no meaning. A model can't tell from the ID alone whether two tokens are related or completely unrelated.

### What embeddings do

Embeddings solve this by representing each token as a vector: a list of floating-point numbers across many dimensions. This turns a meaningless ID into a point in a mathematical "meaning space."

### Meaning maps

In this space, similar words end up close together. "Dog" and "puppy" sit near each other, while an unrelated word like "airplane" sits far away. Distance in this space reflects similarity in meaning.

### The math behind it

Models use **dot products** to measure how close two vectors are. This lets a model match "refund" with "reimbursement" based on semantic similarity, rather than requiring an exact keyword match.

### How embeddings are learned

These vector representations aren't assigned by humans. The model learns them during training by analyzing vast amounts of text, discovering which words tend to appear in similar contexts.

### Why this matters going forward

Embeddings are a foundational concept for topics ahead in the series: semantic search, vector databases, and RAG (Retrieval-Augmented Generation).

## Key takeaways

| Concept | In one line |
| --- | --- |
| Token ID | A unique, meaningless numerical identifier for a token |
| Embedding | A token represented as a multi-dimensional vector |
| Meaning space | A mathematical space where similar words are positioned close together |
| Dot product | The math used to measure similarity/distance between vectors |
| Learned, not assigned | Embeddings are learned from training data, not set by humans |
| What's next | Semantic search, vector databases, RAG |