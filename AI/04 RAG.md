# RAG: Retrieval-Augmented Generation

How RAG lets LLMs answer questions using private, company-specific data without retraining the model.

## Mind map

```mermaid
mindmap
  root((RAG))
    The problem
      Foundation models trained on public data only
      No awareness of private company data
      Retraining per company is costly and slow
    Core concepts
      Chunking
        Large documents split into chunks
        Each chunk becomes an embedding
        Stored in a vector database
      RAG workflow
        User submits a query
        Query converted to embedding
        Vector DB search for relevant chunks
        Chunks passed as context to LLM
        LLM generates informed response
    Why it is essential
      Efficiency
        No training or fine-tuning needed
      Accuracy
        Reduces hallucination
        Grounded in verified facts
      Scalability
        Only relevant info sent
        Avoids information overload
    Design considerations
      Chunk size
        Too large adds noise
        Too small loses context
      Embedding model choice
        Fit for the data type
      Retrieval parameters
        How many chunks to fetch
    Tools and solutions
      Enterprise
        Glean
        Amazon Q
      Open source frameworks
        LangChain
        LlamaIndex
```

## The RAG workflow

```mermaid
flowchart TD
    A["Private documents<br/>e.g. HR policies"] --> B["Chunking<br/>Split into smaller pieces"]
    B --> C["Embedding model<br/>Converts each chunk to a vector"]
    C --> D["Vector database<br/>Stores chunk embeddings"]
    Q["User query"] --> E["Query embedding<br/>Query converted to a vector"]
    E --> F["Similarity search<br/>Finds most relevant chunks"]
    D --> F
    F --> G["Retrieved chunks<br/>Passed as context"]
    Q --> H["LLM"]
    G --> H
    H --> I["Accurate, grounded response"]
```

## Why RAG instead of retraining

```mermaid
flowchart LR
    subgraph Retrain["Retraining the model"]
        r1["Expensive"]
        r2["Time-consuming"]
        r3["Repeated per company"]
    end
    subgraph RAGApproach["RAG approach"]
        g1["No training needed"]
        g2["Fast to set up"]
        g3["Facts grounded, less hallucination"]
    end
```

## Summary

### The problem RAG solves

Organizations often want an AI agent to answer questions using their own private documents, such as HR policies. Foundation models are trained only on public internet data, so they have no awareness of confidential or company-specific information. Retraining a model from scratch for every company is far too costly and time-consuming to be practical.

### Chunking

Large documents are broken down into smaller, manageable pieces called chunks. Each chunk is converted into an embedding, a vector representation, and stored in a vector database.

### The RAG workflow

RAG isn't a specific tool, it's an architectural design pattern:

1. A user submits a query.
2. The system converts the query into an embedding and searches the vector database for the most relevant chunks.
3. Those relevant chunks are passed to the LLM as context, alongside the user's original query.
4. The LLM uses this added context to generate an accurate, informed response.

### Why RAG is essential

- **Efficiency**: avoids the need to train or fine-tune models.
- **Accuracy**: prevents the LLM from hallucinating by grounding it in specific, verified facts.
- **Scalability**: only the information necessary for a specific request is fed to the model, avoiding information overload.

### Design considerations

Building RAG infrastructure involves key trade-offs:

- **Chunk size**: too large can include irrelevant noise; too small can lose context and meaning.
- **Embedding model choice**: the right model depends on the specific data type.
- **Retrieval parameters**: deciding how many chunks to fetch from the database.

### Tools and solutions

Enterprise-level solutions include Glean and Amazon Q. For custom-built solutions, open-source frameworks like LangChain and LlamaIndex are commonly used.

## Key takeaways

| Concept | In one line |
| --- | --- |
| RAG | A design pattern, not a tool, for grounding LLM answers in private data |
| Chunking | Splitting large documents into smaller pieces before embedding |
| Vector database | Stores chunk embeddings and finds the most relevant ones for a query |
| RAG workflow | Query → embedding → retrieval → context → LLM response |
| Efficiency | No need to train or fine-tune the model |
| Accuracy | Reduces hallucination by grounding responses in verified facts |
| Scalability | Only relevant chunks are sent, avoiding information overload |
| Chunk size trade-off | Too large adds noise; too small loses context |
| Example tools | Glean, Amazon Q, LangChain, LlamaIndex |