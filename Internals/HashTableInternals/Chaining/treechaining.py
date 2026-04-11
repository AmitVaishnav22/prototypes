class BSTNode:
    def __init__(self, key, value, hash_value=None):
        self.key = key
        self.value = value
        self.left = None
        self.right = None
        self.hash_value = hash_value

class BST:
    def insert(self,root,key,value,hash_value=None):
        if root is None:
            return BSTNode(key,value,hash_value)
        if key < root.key:
            root.left = self.insert(root.left,key,value,hash_value)
        elif key > root.key:
            root.right = self.insert(root.right,key,value,hash_value)
        else:
            root.value = value
        return root

    def search(self,root,key,hash_value=None):
        if root is None:
            return None
        if root.key == key and root.hash_value == hash_value:
            return root.value
        elif key < root.key:
            return self.search(root.left,key,hash_value)
        else:
            return self.search(root.right,key,hash_value)
    
    def delete(self,root,key,hash_value=None):
        if root is None:
            return root
        if key < root.key:
            root.left = self.delete(root.left,key,hash_value)
        elif key > root.key:
            root.right = self.delete(root.right,key,hash_value)
        else:
            if root.left is None:
                return root.right
            elif root.right is None:
                return root.left
            successor = self.minValueNode(root.right)
            root.key = successor.key
            root.value = successor.value
            root.right = self.delete(root.right, successor.key,successor.hash_value)
        return root
    
    def minValueNode(self,node):
        current = node
        while current.left is not None:
            current = current.left
        return current

class HashTable:
    def __init__(self,size=10):
        self.size=size
        self.table=[None]*self.size
        self.bst=BST()
    def _hash(self,key):
        return hash(key) & 0xfffffff

    def insert(self,key,value):
        h=self._hash(key)
        idx=h%self.size
        self.table[idx]=self.bst.insert(self.table[idx],key,value,h)
    
    def search(self,key):
        h=self._hash(key)
        idx=h%self.size
        return self.bst.search(self.table[idx],key,h)

    def delete(self,key):
        h=self._hash(key)
        idx=h%self.size
        self.table[idx]=self.bst.delete(self.table[idx],key,h)

# Example usage
if __name__ == "__main__":
    ht = HashTable()
    ht.insert("apple", 1)
    ht.insert("banana", 2)
    print(ht.search("apple"))  # Output: 1
    print(ht.search("banana"))  # Output: 2
    ht.delete("apple")
    print(ht.search("apple"))  # Output: None
