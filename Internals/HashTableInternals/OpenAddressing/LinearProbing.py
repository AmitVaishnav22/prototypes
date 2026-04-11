class LinearProbingHashTable:
    def __init__(self, size):
        self.size = size
        self.table = [None] * self.size
    def _hash(self, key):
        return hash(key) & 0xffffffff
    def insert(self, key, value):
        h = self._hash(key)
        idx = h % self.size
        while self.table[idx] is not None:
            if self.table[idx][0] == key:
                self.table[idx] = (key, value)
                return
            idx = (idx + 1) % self.size
        self.table[idx] = (key, value)
    def search(self, key):
        h = self._hash(key)
        idx = h % self.size
        while self.table[idx] is not None:
            if self.table[idx][0] == key:
                return self.table[idx][1]
            idx = (idx + 1) % self.size
        return None
    def delete(self, key):
        h = self._hash(key)
        idx = h % self.size
        while self.table[idx] is not None:
            if self.table[idx][0] == key:
                self.table[idx] = None
                return
            idx = (idx + 1) % self.size
        return None

# Example usage
if __name__ == "__main__":
    ht = LinearProbingHashTable(10)
    ht.insert("apple", 1)
    ht.insert("banana", 2)
    print(ht.search("apple"))  # Output: 1
    print(ht.search("banana"))  # Output: 2
    ht.delete("apple")
    print(ht.search("apple"))  # Output: None

# probing function : idx = (h(key) + i) % size, where i is the number of probes (0, 1, 2, ...)