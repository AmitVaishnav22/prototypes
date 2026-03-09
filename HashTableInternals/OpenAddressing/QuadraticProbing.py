class QuadraticProbingHashTable:
    def __init__(self, size):
        self.size = size
        self.table = [None] * size

    def hash_function(self, key):
        return key % self.size

    def insert(self, key):
        index = self.hash_function(key)
        i = 0
        while self.table[index] is not None:
            if self.table[index] == key:
                return  # Key already exists, do not insert duplicates
            i += 1
            index = (index + i * i) % self.size

        self.table[index] = key

    def search(self, key):
        index = self.hash_function(key)
        i = 0
        while self.table[index] is not None:
            if self.table[index] == key:
                return True
            i += 1
            index = (index + i * i) % self.size
        return False

    def delete(self, key):
        index = self.hash_function(key)
        i = 0
        while self.table[index] is not None:
            if self.table[index] == key:
                self.table[index] = None  # Mark as deleted
                return
            i += 1
            index = (index + i * i) % self.size

# Example usage
if __name__ == "__main__":
    ht = QuadraticProbingHashTable(10)
    ht.insert(1)
    ht.insert(11)
    print(ht.search(1))  # Output: True
    print(ht.search(11))  # Output: True
    print(ht.search(21))  # Output: False
    ht.delete(1)
    print(ht.search(1))  # Output: False


# probing function : idx = (h(key) + i^2) % size, where i is the number of probes (0, 1, 2, ...)