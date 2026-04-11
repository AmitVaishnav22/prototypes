class DoubleHashTable:
    def __init__(self, size):
        self.size = size
        self.table = [None] * size
    def hash_function1(self, key):
        return key % self.size
    def hash_function2(self, key):
        return 1 + (key % (self.size - 1))
    def insert(self, key):
        index1 = self.hash_function1(key)
        index2 = self.hash_function2(key)
        i = 0
        while self.table[index1] is not None:
            if self.table[index1] == key:
                return  # Key already exists, do not insert duplicates
            i += 1
            index1 = (index1 + i * index2) % self.size
        self.table[index1] = key
    def search(self, key):
        index1 = self.hash_function1(key)
        index2 = self.hash_function2(key)
        i = 0
        while self.table[index1] is not None:
            if self.table[index1] == key:
                return True
            i += 1
            index1 = (index1 + i * index2) % self.size
        return False
    def delete(self, key):
        index1 = self.hash_function1(key)
        index2 = self.hash_function2(key)
        i = 0
        while self.table[index1] is not None:
            if self.table[index1] == key:
                self.table[index1] = None  # Mark as deleted
                return
            i += 1
            index1 = (index1 + i * index2) % self.size
# Example usage
if __name__ == "__main__":
    ht = DoubleHashTable(10)
    ht.insert(1)
    ht.insert(11)
    print(ht.search(1))  # Output: True
    print(ht.search(11))  # Output: True
    print(ht.search(21))  # Output: False
    ht.delete(1)
    print(ht.search(1))  # Output: False

