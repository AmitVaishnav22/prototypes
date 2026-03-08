class ListNode:
    def __init__(self, key, value, hash_value=None):
        self.key=key
        self.value = value
        self.hash_value = hash_value
        self.next = None

class HashTable:
    def __init__(self, size=10):
        self.size = size
        self.table = [None] * self.size

    def _hash(self, key):
        return hash(key) & 0xffffffff

    def insert(self, key, value):
        h=self._hash(key)
        idx=h%self.size
        curr=self.table[idx]
        while curr:
            if curr.hash_value==h and curr.key==key:
                curr.value=value
                return
            prev=curr
            curr=curr.next
        new_node=ListNode(key,value,h)
        new_node.next=self.table[idx]
        self.table[idx]=new_node

    def search(self, key):
        h=self._hash(key)
        idx=h%self.size
        curr=self.table[idx]
        while curr:
            if curr.hash_value==h and curr.key==key:
                return curr.value
            curr=curr.next
        return None
    
    def delete(self, key):
        h=self._hash(key)
        idx=h%self.size
        curr=self.table[idx]
        prev=None
        while curr:
            if curr.hash_value==h and curr.key==key:
                if prev:
                    prev.next=curr.next
                else:
                    self.table[idx]=curr.next
                return
            prev=curr
            curr=curr.next
        return None

# Example usage
if __name__ == "__main__":
    ht = HashTable()
    ht.insert("apple", 1)
    ht.insert("banana", 2)
    print(ht.search("apple"))  # Output: 1
    print(ht.search("banana"))  # Output: 2
    ht.delete("apple")
    print(ht.search("apple"))  # Output: None


# This implementation of a hash table uses chaining to handle collisions. Each bucket in the hash table can contain a linked list of nodes, allowing multiple key-value pairs to be stored in the same bucket if they hash to the same index. The `insert`, `search`, and `delete` methods manage the linked list appropriately to maintain the integrity of the hash table.
# complexity:
# - Insertion: O(1) on average, O(n) in the worst case (when all keys hash to the same index).
# - Search: O(1) on average, O(n) in the worst case.
# - Deletion: O(1) on average, O(n) in the worst case
# space complexity: O(n) where n is the number of key-value pairs stored in the hash table.
    