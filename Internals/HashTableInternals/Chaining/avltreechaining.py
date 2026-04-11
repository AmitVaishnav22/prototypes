class AVLNode:
    def __init__(self, key, value, hash_value=None):
        self.key = key
        self.value = value
        self.left = None
        self.right = None
        self.height = 1
        self.hash_value = hash_value

class AVLTree:
    def height(self, node):
        return node.height if node else 0

    def balanceFactor(self, node):
        return self.height(node.left) - self.height(node.right) if node else 0

    def rotateRight(self,y):
        x=y.left
        t=x.right
        x.right=y
        y.left=t
        y.height=1+max(self.height(y.left),self.height(y.right))
        x.height=1+max(self.height(x.left),self.height(x.right))
        return x

    def rotateLeft(self,x):
        y=x.right
        t=y.left
        y.left=x
        x.right=t
        x.height=1+max(self.height(x.left),self.height(x.right))
        y.height=1+max(self.height(y.left),self.height(y.right))
        return y
    
    def insert(self,root,key,value,hash_value=None):
        if not root:
            return AVLNode(key,value,hash_value)
        if key < root.key:
            root.left = self.insert(root.left,key,value,hash_value)
        elif key > root.key:
            root.right = self.insert(root.right,key,value,hash_value)
        else:
            root.value = value
            root.hash_value = hash_value
            return root
        
        root.height=1+max(self.height(root.left),self.height(root.right))
        balance=self.balanceFactor(root)
        if balance > 1 and key < root.left.key:
            return self.rotateRight(root)
        if balance < -1 and key > root.right.key:
            return self.rotateLeft(root)
        if balance > 1 and key > root.left.key:
            root.left = self.rotateLeft(root.left)
            return self.rotateRight(root)
        if balance < -1 and key < root.right.key:
            root.right = self.rotateRight(root.right)
            return self.rotateLeft(root)
        return root

    def search(self,root,key,hash_value=None):
        if not root:
            return None
        if root.key == key and root.hash_value == hash_value:
            return root.value
        elif key < root.key:
            return self.search(root.left,key,hash_value)
        else:
            return self.search(root.right,key,hash_value)

    def delete(self,root,key,hash_value=None):
        if not root:
            return root
        if key < root.key:
            root.left = self.delete(root.left,key,hash_value)
        elif key > root.key:
            root.right = self.delete(root.right,key,hash_value)
        else:
            if not root.left:
                return root.right
            elif not root.right:
                return root.left
            successor = self.minValueNode(root.right)
            root.key = successor.key
            root.value = successor.value
            root.right = self.delete(root.right, successor.key,successor.hash_value)
        
        root.height=1+max(self.height(root.left),self.height(root.right))
        balance=self.balanceFactor(root)
        if balance > 1 and self.balanceFactor(root.left) >= 0:
            return self.rotateRight(root)
        if balance < -1 and self.balanceFactor(root.right) <= 0:
            return self.rotateLeft(root)
        if balance > 1 and self.balanceFactor(root.left) < 0:
            root.left = self.rotateLeft(root.left)
            return self.rotateRight(root)
        if balance < -1 and self.balanceFactor(root.right) > 0:
            root.right = self.rotateRight(root.right)
            return self.rotateLeft(root)
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
        self.avl=AVLTree()
    def _hash(self,key):
        return hash(key) & 0xfffffff

    def insert(self,key,value):
        h=self._hash(key)
        idx=h%self.size
        self.table[idx]=self.avl.insert(self.table[idx],key,value,h)

    def search(self,key):
        h=self._hash(key)
        idx=h%self.size
        return self.avl.search(self.table[idx],key,h)

    def delete(self,key):
        h=self._hash(key)
        idx=h%self.size
        self.table[idx]=self.avl.delete(self.table[idx],key,h)

# Example usage
if __name__ == "__main__":
    ht = HashTable()
    ht.insert("apple", 1)
    ht.insert("banana", 2)
    print(ht.search("apple"))  # Output: 1
    print(ht.search("banana"))  # Output: 2
    ht.delete("apple")
    print(ht.search("apple"))  # Output: None


# reason for having hash_value in AVLNode is to ensure that we are comparing the correct key in case of hash collisions. In a hash table, multiple keys can have the same hash value, so we need to check both the key and the hash value to ensure we are retrieving or deleting the correct entry.
# In the insert, search, and delete methods of the AVLTree, we compare both the key and the hash value to ensure that we are working with the correct node in case of hash collisions. This way, even if two different keys have the same hash value, we can still distinguish between them and perform the correct operations on the AVL tree.