# 微软面试高频算法题（C# 版）

**目标**：涵盖LeetCode前200道中微软最常考的题目
**难度**：Easy ~ Medium
**准备时间**：3天冲刺版本

---

## 目录

1. [二叉树遍历](#1-二叉树遍历)
2. [链表操作](#2-链表操作)
3. [数组和字符串](#3-数组和字符串)
4. [查找和排序](#4-查找和排序)
5. [设计题](#5-设计题)
6. [动态规划](#6-动态规划)

---

## 1. 二叉树遍历

### 题目 1.1：二叉树的中序遍历（LeetCode 94）

**难度**：Easy | **频率**：⭐⭐⭐⭐⭐

**题目描述**
```
给定一个二叉树的根节点 root，返回它的 中序遍历。
中序遍历：左 -> 根 -> 右
```

**核心思路**
1. **理解遍历顺序**：中序 = 左子树 -> 当前节点 -> 右子树
2. **选择合适方法**：
   - 递归简单直接（推荐面试使用）
   - 迭代需要手动管理栈（展现深入理解）
   - Morris只在面试官要求O(1)空间时使用
3. **关键点**：保持三部分顺序不能错

**答案思路**
- **递归**：最推荐 - 代码简洁，易于理解和调试
- **迭代**：用栈模拟递归过程 - 向左走到底，再处理根和右
- **Morris遍历**：O(1)空间（进阶，一般不问）

**易错点**
- ❌ 中序顺序写错（很常见！记住：左、根、右）
- ❌ 迭代时忘记处理右子树
- ❌ Base case 判断错误（null节点处理）

**C# 递归解决方案**
```csharp
public IList<int> InorderTraversal(TreeNode root) {
    var result = new List<int>();
    InorderHelper(root, result);
    return result;
}

private void InorderHelper(TreeNode node, List<int> result) {
    if (node == null) return;
    InorderHelper(node.left, result);      // 左
    result.Add(node.val);                   // 根
    InorderHelper(node.right, result);     // 右
}
```

**C# 迭代解决方案**
```csharp
public IList<int> InorderTraversal(TreeNode root) {
    var result = new List<int>();
    var stack = new Stack<TreeNode>();
    var current = root;
    
    while (current != null || stack.Count > 0) {
        // 向左走到底
        while (current != null) {
            stack.Push(current);
            current = current.left;
        }
        
        // 弹出节点，访问，向右走
        current = stack.Pop();
        result.Add(current.val);
        current = current.right;
    }
    
    return result;
}
```

**复杂度分析**
- 时间复杂度：O(n)，访问每个节点一次
- 空间复杂度：O(h)，h为树的高度（递归栈）

**面试建议**
- 先说出递归方案（简单、安全）
- 如果时间充足，展示迭代方案（展现深度）
- 解释为什么迭代时需要特殊处理根节点

**图形讲解**
```
       1
      / \
     2   3
    
迭代过程：
1. 推栈：1 -> 2 (栈: [1,2])
2. 推栈：null (栈: [1,2])
3. 弹出2，访问2 (结果: [2])
4. 向右：null (栈: [1])
5. 弹出1，访问1 (结果: [2,1])
6. 向右：3 (栈: [3])
7. 弹出3，访问3 (结果: [2,1,3])

最终：[2, 1, 3]（左-根-右）
```

---

### 题目 1.2：二叉树的层序遍历（LeetCode 102）

**难度**：Medium | **频率**：⭐⭐⭐⭐⭐

**题目描述**
```
给定一个二叉树，返回其按 层序遍历 得到的节点值（逐层从左到右）。
```

**核心思路**
1. **BFS vs DFS**：这道题明确要求"按层"，所以只能用BFS（队列）
2. **关键技巧**：记录当前层的节点数（levelSize），确保一次循环恰好处理一层
3. **为什么这样做**：
   - 如果不记录levelSize，会处理不完整
   - levelSize = queue.Count，在处理前捕获

**答案思路**
- **使用队列（BFS）**：这是唯一正确的方向
- **关键**：每次处理一层的所有节点前，先记录队列大小
- **流程**：记录数量 -> 循环处理一层 -> 加入下一层 -> 结果加层

**易错点**
- ❌ 忘记记录levelSize，导致一次处理多层
- ❌ 在循环中动态获取queue.Count（会变化）
- ❌ 子节点为null时没有跳过（判断不够）

**C# 解决方案**
```csharp
public IList<IList<int>> LevelOrder(TreeNode root) {
    var result = new List<IList<int>>();
    if (root == null) return result;
    
    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);
    
    while (queue.Count > 0) {
        int levelSize = queue.Count;  // 当前层的节点数
        var level = new List<int>();
        
        // 处理当前层的所有节点
        for (int i = 0; i < levelSize; i++) {
            var node = queue.Dequeue();
            level.Add(node.val);
            
            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }
        
        result.Add(level);
    }
    
    return result;
}
```

**复杂度分析**
- 时间复杂度：O(n)，访问所有节点
- 空间复杂度：O(w)，w为树的最大宽度（最坏情况满二叉树底层）

**面试建议**
- 一定要解释为什么用levelSize这个技巧
- 如果写错了，面试官会要求改进，这时说出problemSolving过程

**图形讲解**
```
       1          第1层
      / \
     2   3        第2层
    / \
   4   5          第3层

层序遍历结果：[[1], [2,3], [4,5]]

队列过程：
第1次迭代：队列[1] -> 弹出1 -> 入队[2,3] -> 结果[[1]]
第2次迭代：队列[2,3] -> 弹出2,3 -> 入队[4,5] -> 结果[[1],[2,3]]
第3次迭代：队列[4,5] -> 弹出4,5 -> 入队[] -> 结果[[1],[2,3],[4,5]]
```

---

### 题目 1.3：二叉树的后序遍历（LeetCode 145）

**难度**：Easy | **频率**：⭐⭐⭐⭐

**题目描述**
```
给定一个二叉树的根节点，返回它的 后序遍历。
后序遍历：左 -> 右 -> 根
```

**答案思路**
- 递归最简洁
- 迭代：使用栈，关键是判断什么时候访问根节点

**C# 递归解决方案**
```csharp
public IList<int> PostorderTraversal(TreeNode root) {
    var result = new List<int>();
    PostorderHelper(root, result);
    return result;
}

private void PostorderHelper(TreeNode node, List<int> result) {
    if (node == null) return;
    PostorderHelper(node.left, result);     // 左
    PostorderHelper(node.right, result);    // 右
    result.Add(node.val);                    // 根
}
```

**C# 迭代解决方案**（两栈法）
```csharp
public IList<int> PostorderTraversal(TreeNode root) {
    var result = new List<int>();
    if (root == null) return result;
    
    var stack1 = new Stack<TreeNode>();
    var stack2 = new Stack<TreeNode>();
    stack1.Push(root);
    
    // 通过两个栈反向得到后序
    while (stack1.Count > 0) {
        var node = stack1.Pop();
        stack2.Push(node);
        
        if (node.left != null) stack1.Push(node.left);
        if (node.right != null) stack1.Push(node.right);
    }
    
    while (stack2.Count > 0) {
        result.Add(stack2.Pop().val);
    }
    
    return result;
}
```

---

## 2. 链表操作

### 题目 2.1：反转链表（LeetCode 206）

**难度**：Easy | **频率**：⭐⭐⭐⭐⭐

**题目描述**
```
给你单链表的头节点 head，请你反转链表，并返回反转后的链表。

示例：1 -> 2 -> 3 -> null
反转：3 -> 2 -> 1 -> null
```

**深度解题思路**
1. **问题分析**：
   - 链表的每个节点的next指针需要反向
   - 需要保存下一个节点（否则丢失）
   - 需要知道前一个节点

2. **三指针法思路**（迭代，推荐）：
   - `prev`：当前节点要指向的节点
   - `curr`：正在处理的节点
   - `next`：保存curr的下一个节点（反转前保存）
   - 循环：反转curr->next，然后三个指针都前移一步

3. **递归思路**：
   - 向下递归到最后一个节点
   - 回溯时反转指针
   - 需要小心处理边界（避免循环指向）

**答案思路**
- **迭代（推荐）**：三指针法，直观易调试
- **递归**：展示理解深度，但容易出错

**C# 迭代解决方案（推荐）**
```csharp
public ListNode ReverseList(ListNode head) {
    ListNode prev = null;
    ListNode curr = head;
    
    while (curr != null) {
        // 保存下一个节点
        ListNode nextTemp = curr.next;
        // 反转当前节点
        curr.next = prev;
        // 前进
        prev = curr;
        curr = nextTemp;
    }
    
    return prev;
}
```

**C# 递归解决方案**
```csharp
public ListNode ReverseList(ListNode head) {
    if (head == null || head.next == null) return head;
    
    // 递归到最后一个节点
    ListNode newHead = ReverseList(head.next);
    
    // 反转指向
    head.next.next = head;
    head.next = null;
    
    return newHead;
}
```

**复杂度分析**
- 时间复杂度：O(n)，访问所有n个节点
- 空间复杂度：O(1)（迭代）/ O(n)（递归栈深度）

**易错点常见修复**
- ❌ 丢失节点：要在改变next前保存nextTemp
- ❌ 返回值错误：迭代返回prev，递归需特殊处理
- ❌ 无限循环：反转后next没有更新

**面试建议顺序**
1. 先讲三指针迭代方案（安全、清晰）
2. 给出示例手工走一遍（让面试官放心）
3. 时间充足时提及递归（加分项）

**图形讲解**
```
反转过程：

初始：null <- 1   2 -> 3 -> null
                  ↑

第1步：null <- 1 <- 2   3 -> null
              ↑    ↑

第2步：null <- 1 <- 2 <- 3   null
                   ↑    ↑

最终：3 <- 2 <- 1   null
           ↑

三指针法详解：
prev  curr  next
null   1  -> 2
      ↓
null <- 1    2 -> 3
         ↑   ↑
```

---

### 题目 2.2：合并两个有序链表（LeetCode 21）

**难度**：Easy | **频率**：⭐⭐⭐⭐⭐

**题目描述**
```
将两个有序链表合并为一个新的有序链表并返回。

列表1：1 -> 2 -> 4
列表2：1 -> 3 -> 4
合并：1 -> 1 -> 2 -> 3 -> 4 -> 4
```

**答案思路**
- 使用双指针，逐个比较两个链表节点
- 将较小的节点加入结果链表

**C# 解决方案**
```csharp
public ListNode MergeTwoLists(ListNode list1, ListNode list2) {
    // 创建哨兵节点，便于处理
    ListNode dummy = new ListNode(0);
    ListNode current = dummy;
    
    while (list1 != null && list2 != null) {
        if (list1.val < list2.val) {
            current.next = list1;
            list1 = list1.next;
        } else {
            current.next = list2;
            list2 = list2.next;
        }
        current = current.next;
    }
    
    // 连接剩余部分
    current.next = list1 != null ? list1 : list2;
    
    return dummy.next;
}
```

**复杂度分析**
- 时间复杂度：O(n + m)
- 空间复杂度：O(1)

---

## 3. 数组和字符串

### 题目 3.1：两数之和（LeetCode 1）

**难度**：Easy | **频率**：⭐⭐⭐⭐⭐

**题目描述**
```
给定一个整数数组 nums 和一个整数目标值 target，
请你在该数组中找出 和为目标值 target 的那两个整数，
并返回它们的数组下标。

示例：nums = [2,7,11,15], target = 9
输出：[0,1]（2 + 7 = 9）
```

**关键解题思路**

1. **为什么不能用排序？**
   - 题目要求返回"数组下标"
   - 排序会破坏原始下标
   - 所以不能用双指针法

2. **暴力 O(n²) 的问题**
   - 两层循环检查所有配对
   - 在数组很大时超时

3. **哈希表 O(n) 的关键洞察**（最优）
   - 问题转化：对于每个num，找是否存在 (target - num)
   - 一遍扫描：边检查边记录
   - **技巧**：先检查再存储，避免重复配对

**答案思路**

- **哈希表**（最优）：一遍扫描O(n)，无法排序的问题首选
- **暴力**：O(n²)，仅用于不在意复杂度的场景
- **排序+双指针**：思路清晰但不适合此题（下标会变）

**C# 哈希表解决方案**
```csharp
public int[] TwoSum(int[] nums, int target) {
    var map = new Dictionary<int, int>();
    
    for (int i = 0; i < nums.Length; i++) {
        int complement = target - nums[i];
        
        if (map.ContainsKey(complement)) {
            return new int[] { map[complement], i };
        }
        
        if (!map.ContainsKey(nums[i])) {
            map[nums[i]] = i;
        }	
    }
    
    return new int[] { };
}
```

**复杂度分析**
- 时间复杂度：O(n)，单遍扫描
- 空间复杂度：O(n)，最坏情况存储所有元素

**面试时的讨论点**
- 为什么选择哈希表而不是排序？（答：下标）
- 为什么先检查再存储？（答：避免num+num=target的错误）
- 能否有其他时间复杂度？（答：O(n)已是最优）

**图形讲解**
```
nums = [2,7,11,15], target = 9

遍历过程：
i=0: num=2, complement=7, map={2:0}
i=1: num=7, complement=2, 找到! 返回[0,1]

哈希表状态：
     值  索引
{    2:  0
     7:  1  <- 找到2
}
```

---

### 题目 3.2：字符串反转（LeetCode 151）

**难度**：Medium | **频率**：⭐⭐⭐⭐

**题目描述**
```
给定一个字符串，将字符串中每个单词的顺序反转，
但单词内字符顺序不变。多余空格也要处理。

示例："the sky is blue"
输出："blue is the sky"
```

**深度思路分析**

1. **理解问题**：
   - 需要反转单词顺序（不是字符！）
   - 要处理多余空格（"a  b" -> "b a"）
   - 通常还要求O(n)时间 O(1)空间（如果可能）

2. **不同方案对比**：
   - **Split+Reverse**：简单但用O(n)额外空间
   - **反转两次**：O(n)时间O(n)空间（如果用char[]），更优雅
   - 选择：面试时优先简便方案（正确性>优化）

3. **"反转两次"的巧妙性**：
   - 整体反转："blue is the sky" 
   - 反转每个单词："blue is the sky"（回到目标！）

**答案思路**
- **最优理想方案**：整体反转 + 单词反转（思路优雅）
- **实用方案**：Split + Reverse + Join（正确率高）
- **选择建议**：面试优先选简便方案，说出最优思路

**C# 最优解决方案**
```csharp
public string ReverseWords(string s) {
    // 移除多余空格
    s = s.Trim();
    char[] chars = s.ToCharArray();
    
    // 反转整个字符串
    Reverse(chars, 0, chars.Length - 1);
    
    // 反转每个单词
    int start = 0;
    for (int i = 0; i <= chars.Length; i++) {
        if (i == chars.Length || chars[i] == ' ') {
            Reverse(chars, start, i - 1);
            start = i + 1;
        }
    }
    
    // 移除多余空格
    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < chars.Length; i++) {
        if (chars[i] != ' ' || (i > 0 && chars[i - 1] != ' ')) {
            sb.Append(chars[i]);
        }
    }
    
    return sb.ToString().Trim();
}

private void Reverse(char[] chars, int start, int end) {
    while (start < end) {
        char temp = chars[start];
        chars[start] = chars[end];
        chars[end] = temp;
        start++;
        end--;
    }
}
```

**C# 简便解决方案**
```csharp
public string ReverseWords(string s) {
    string[] words = s.Trim().Split(new char[] {' '}, 
                                    StringSplitOptions.RemoveEmptyEntries);
    System.Array.Reverse(words);
    return string.Join(" ", words);
}
```

**复杂度分析**
- 最优方案：O(n) 时间，O(1)空间（原地操作）或O(n)空间（char数组）
- 简便方案：O(n) 时间，O(n) 空间（Split生成数组）

**面试建议**
- 如果要求O(1)空间：用两次反转法
- 否则：直接用Split+Reverse（快速得分）
- 能说出最优思路是加分项

**图形讲解**
```
原始字符串："the sky is blue"

步骤1 - 反转整个字符串：
"the sky is blue" -> "eulb si yks eht"

步骤2 - 反转每个单词：
"eulb" -> "blue"
"si" -> "is"
"yks" -> "sky"
"eht" -> "the"

最终："blue is sky the"
```

---

### 题目 3.3：最长回文子串（LeetCode 5）

**难度**：Medium | **频率**：⭐⭐⭐⭐

**题目描述**
```
给你一个字符串 s，找到 s 中最长的回文子串。

示例：s = "babad"
输出："bab" 或 "aba"
```

**核心解题思路**

1. **问题洞察**：
   - 回文的特点：从中心向外对称
   - 中心可能是一个字符（奇数长度）或两个字符（偶数长度）
   - 这启发我们用"中心扩展"思路

2. **三种方案对比**：
   | 方案 | 时间 | 空间 | 难度 | 面试推荐 |
   |-----|------|------|------|----------|
   | 中心扩展 | O(n²) | O(1) | ★★☆ | ✓ 推荐 |
   | 动态规划 | O(n²) | O(n²) | ★★★ | 也可以 |
   | Manacher | O(n) | O(n) | ★★★★☆ | 进阶 |

3. **为什么推荐中心扩展**：
   - 代码简洁易写
   - 空间复杂度最优
   - 容易解释和调试

**答案思路**
- **中心扩展**（推荐）：遍历每个可能的中心，向外扩展比较
- **动态规划**：考虑子串关系，自下而上构建
- **Manacher**：O(n)但较复杂，一般不问

**C# 中心扩展解决方案**
```csharp
public string LongestPalindrome(string s) {
    if (s.Length < 2) return s;
    
    int start = 0, maxLen = 1;
    
    for (int i = 0; i < s.Length; i++) {
        // 奇数长度回文（单个中心）
        int len1 = ExpandAroundCenter(s, i, i);
        // 偶数长度回文（两个中心）
        int len2 = ExpandAroundCenter(s, i, i + 1);
        
        int len = Math.Max(len1, len2);
        
        if (len > maxLen) {
            maxLen = len;
            start = i - (len - 1) / 2;
        }
    }
    
    return s.Substring(start, maxLen);
}

private int ExpandAroundCenter(string s, int left, int right) {
    while (left >= 0 && right < s.Length && s[left] == s[right]) {
        left--;
        right++;
    }
    return right - left - 1;  // 回文长度
}
```

**复杂度分析**
- 时间复杂度：O(n²)，n个中心，每个中心O(n)扩展
- 空间复杂度：O(1)，只用几个指针

**易错点和调试**
- ❌ 忘记同时考虑奇偶长度（len1 和 len2）
- ❌ 计算起始位置时出错：start = i - (len-1)/2
- ✓ 调试技巧：用小样例手工走一遍

**图形讲解**
```
字符串："babad"

中心扩展过程：
索引：0   1   2   3   4
字符：b   a   b   a   d

从索引2（'b'）开始：
    中心：b
    扩展1：a b a（回文！长度3）
    扩展2：b a b a（'b'在外，不匹配，停止）

找到 "bab" 长度3
继续...最终找到最长的一个
```

---

## 4. 查找和排序

### 题目 4.1：二分查找（LeetCode 704）

**难度**：Easy | **频率**：⭐⭐⭐⭐⭐

**题目描述**
```
给定一个 n 个元素有序的（升序）整型数组 nums 和一个目标值 target，
写一个函数搜索 nums 中的 target，如果目标值存在返回下标，否则返回 -1。
```

**关键思路讲解**

1. **为什么二分有效**：
   - 数组有序（这是前提！）
   - 每次排除一半数据
   - 递归/循环进行

2. **模板的细节**：
   - `int mid = left + (right - left) / 2`：防止整数溢出
   - `left <= right`：循环条件（包含只剩1个元素的情况）
   - `left = mid + 1` 和 `right = mid - 1`：避免无限循环

3. **边界问题常见错误**：
   - 循环条件：left < right vs left <= right
   - mid 更新：mid+1 vs mid-1 很关键
   - 返回值：找不到时如何处理

**答案思路**
- **标准模板**：掌握一个可靠的模板
- **关键点**：mid计算防溢出，left/right更新别出错
- **面试技巧**：写出一个能运行的版本比背完美方案更重要

**C# 解决方案**
```csharp
public int Search(int[] nums, int target) {
    int left = 0, right = nums.Length - 1;
    
    while (left <= right) {
        int mid = left + (right - left) / 2;  // 防止溢出
        
        if (nums[mid] == target) {
            return mid;
        } else if (nums[mid] < target) {
            left = mid + 1;
        } else {
            right = mid - 1;
        }
    }
    
    return -1;
}
```

**复杂度分析**
- 时间复杂度：O(log n)，非常高效
- 空间复杂度：O(1)，仅用几个指针

**面试常见变体**
- 找第一个>=target的位置
- 找最后一个<=target的位置
- 判断是否存在target

都用同一个模板，只调整比较逻辑

**图形讲解**
```
nums = [−1,0,3,5,9,12], target = 9

      left      mid      right
第1轮：0   ->   3    ->  5
      nums[3]=5 < 9, left=4

第2轮：4   ->   4    ->  5
      nums[4]=9 == 9, 返回4

过程图：
[-1|0|3|5|9|12]
 L     M     R
 ->
      L M R
      -> 找到!
```

---

### 题目 4.2：快速排序（自定义实现）

**难度**：Medium | **频率**：⭐⭐⭐⭐

**题目描述**
```
实现快速排序算法。

示例：[3,2,1,5,6,4] -> [1,2,3,4,5,6]
```

**答案思路**
- **分治**：选择枢纽，分区，递归
- **关键**：分区函数（partition）
- **优化**：随机选择枢纽，三路分区

**C# 快速排序解决方案**
```csharp
public void QuickSort(int[] nums) {
    if (nums == null || nums.Length == 0) return;
    QuickSort(nums, 0, nums.Length - 1);
}

private void QuickSort(int[] nums, int low, int high) {
    if (low < high) {
        int pi = Partition(nums, low, high);
        QuickSort(nums, low, pi - 1);
        QuickSort(nums, pi + 1, high);
    }
}

private int Partition(int[] nums, int low, int high) {
    int pivot = nums[high];
    int i = low - 1;
    
    for (int j = low; j < high; j++) {
        if (nums[j] < pivot) {
            i++;
            // 交换
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
    }
    
    // 将枢纽放到正确位置
    int temp2 = nums[i + 1];
    nums[i + 1] = nums[high];
    nums[high] = temp2;
    
    return i + 1;
}
```

**C# 优化版本（随机枢纽）**
```csharp
private Random random = new Random();

private int PartitionRandom(int[] nums, int low, int high) {
    int randomIndex = low + random.Next(high - low + 1);
    // 将随机元素与最后一个元素交换
    int temp = nums[randomIndex];
    nums[randomIndex] = nums[high];
    nums[high] = temp;
    
    return Partition(nums, low, high);
}
```

**复杂度分析**
- 平均时间复杂度：O(n log n)
- 最坏时间复杂度：O(n²)（已排序的数组）
- 空间复杂度：O(log n)（递归栈，平均）

**图形讲解**
```
原始数组：[3,2,1,5,6,4]
选择枢纽：4（最后一个）

分区过程：
小于4：[1,2,3] | 4 | 大于等于4：[5,6]

递归：
[3,2,1]              [5,6]
分区：1             分区：5
[1] 2 [3]           [5] 6 []

最终排序后：[1,2,3,4,5,6]

分区图示（第一次）：
数组：[3,2,1,5,6|4]
      i    j  pivot=4

i=-1, j=0: nums[0]=3 < 4, i=0, 交换(无)
i=0, j=1: nums[1]=2 < 4, i=1, 交换(2,3)-> [2,3,1,5,6|4]
i=1, j=2: nums[2]=1 < 4, i=2, 交换(1,1)-> [2,3,1,5,6|4]
i=2, j=3: nums[3]=5 > 4, 跳过
i=2, j=4: nums[4]=6 > 4, 跳过

最后交换nums[i+1]与pivot: [2,3,1,4,6,5]（不完全正确，概念演示）
```

---

## 5. 设计题

### 题目 5.1：LRU 缓存（LeetCode 146）

**难度**：Medium | **频率**：⭐⭐⭐⭐⭐

**题目描述**
```
设计一个LRU（最近最少使用）缓存类。

实现以下操作：
- LRUCache(capacity): 初始化LRU编制，正整数作为容量 capacity
- int get(key): 如果关键字 key 存在于缓存中，则返回关键字的值，否则返回 -1
- void put(key, value): 如果关键字已经存在，则变更其数据值；
             如果关键字不存在，则插入该组「关键字-值」。
             当缓存容量达到上限时，它应该在写入新数据之前删除最久未使用的数据值。

上述两个操作都应该分别以 O(1) 的平均时间复杂度来完成。
```

**深度解题思路**

1. **问题分析**：
   - 需要 O(1) 查找：→ 用 HashMap
   - 需要 O(1) 删除：→ 不能用数组或单向链表
   - 需要维护"最近使用"顺序：→ 双向链表
   - Get时要移到最前，Put新项也要放最前
   - 容量满时删除最后一个（最久未用）

2. **为什么是HashMap + 双向链表**：
   - HashMap(value = 链表Node)：快速找到Node
   - 双向链表：O(1)删除Node并重新添加
   - Head:最近用，Tail:最久未用

3. **关键操作**：
   - `RemoveNode`：从链表中删除
   - `AddToHead`：添加到链表头部
   - Get/Put都要调用这两个操作

4. **常见实现细节错误**：
   - 忘记更新HashMap中的引用
   - Get时忘记移到Head
   - Put时新值忘记移到Head

**答案思路**
- **唯一正确结构**：HashMap + 双向链表
  - HashMap 快速查找和删除key
  - 双向链表维护LRU顺序
- **关键操作**：RemoveNode和AddToHead的正确实现
- **思考顺序**：先实现add/remove工具，再实现get/put

**C# 解决方案**
```csharp
public class LRUCache {
    private class Node {
        public int Key;
        public int Value;
        public Node Prev;
        public Node Next;
        
        public Node(int key, int value) {
            Key = key;
            Value = value;
        }
    }
    
    private int capacity;
    private Dictionary<int, Node> cache;
    private Node head;  // 最近使用
    private Node tail;  // 最久未使用
    
    public LRUCache(int capacity) {
        this.capacity = capacity;
        cache = new Dictionary<int, Node>();
        head = new Node(-1, -1);
        tail = new Node(-1, -1);
        head.Next = tail;
        tail.Prev = head;
    }
    
    public int Get(int key) {
        if (!cache.ContainsKey(key)) {
            return -1;
        }
        
        Node node = cache[key];
        RemoveNode(node);
        AddToHead(node);
        
        return node.Value;
    }
    
    public void Put(int key, int value) {
        if (cache.ContainsKey(key)) {
            // 更新值
            Node node = cache[key];
            node.Value = value;
            RemoveNode(node);
            AddToHead(node);
        } else {
            // 新增
            if (cache.Count >= capacity) {
                // 删除最久未使用（尾部）
                RemoveNode(tail.Prev);
                cache.Remove(tail.Prev.Key);
            }
            
            Node newNode = new Node(key, value);
            cache[key] = newNode;
            AddToHead(newNode);
        }
    }
    
    private void RemoveNode(Node node) {
        node.Prev.Next = node.Next;
        node.Next.Prev = node.Prev;
    }
    
    private void AddToHead(Node node) {
        node.Next = head.Next;
        head.Next.Prev = node;
        head.Next = node;
        node.Prev = head;
    }
}
```

**复杂度分析**
- 时间复杂度：Get O(1)，Put O(1)（HashMap和链表操作都是O(1)）
- 空间复杂度：O(capacity)，存储最多capacity个节点

**面试建议**
- 这是设计题中的经典，微软很爱问
- 先说出数据结构（HashMap + 链表）
- 再讲解为什么这样设计
- 最后写代码（可能需要15分钟）
- 如果卡住，向面试官询问"我是否理解对了需求"

**图形讲解**
```
LRUCache cache(2)

put(1, 1): 
  HashMap: {1:node}
  链表: head <-> node(1,1) <-> tail

put(2, 2):
  HashMap: {1:node1, 2:node2}
  链表: head <-> node2(2,2) <-> node1(1,1) <-> tail

get(1):
  返回1
  更新：head <-> node1(1,1) <-> node2(2,2) <-> tail

put(3, 3):  容量满了
  删除node2（最久未使用）
  HashMap: {1:node1, 3:node3}
  链表: head <-> node3(3,3) <-> node1(1,1) <-> tail

get(2):
  返回-1（已删除）
```

---

## 6. 动态规划

### 题目 6.1：爬楼梯（LeetCode 70）

**难度**：Easy | **频率**：⭐⭐⭐⭐

**题目描述**
```
假设你正在爬楼梯。需要 n 阶你才能到达楼顶。
每次你可以爬 1 或 2 个台阶。
有多少种不同的方法可以爬到楼顶？

示例：n = 3
输出：3
解释：1+1+1, 1+2, 2+1
```

**DP思路讲解**

1. **关键洞察**：
   - 到达第n阶只有两种来法：从n-1阶跨1步，或从n-2阶跨2步
   - 所以 `f(n) = f(n-1) + f(n-2)` = 斐波那契！

2. **建立DP**：
   - `dp[i]` = 爬到第i阶的方法数
   - Base case：`dp[1]=1, dp[2]=2`
   - 递推：`dp[i] = dp[i-1] + dp[i-2]`

3. **优化空间**：
   - 只需要前两个值，不需要整个数组
   - 可降空间 O(n) → O(1)

4. **注意事项**：
   - 边界处理（n=1, n=2)
   - 数据溢出（大数时）

**答案思路**
- **递推关系**：f(n) = f(n-1) + f(n-2)
- **DP数组**：dp[i]表示爬到i阶的方法数
- **空间优化**：只需保存前两个值

**C# 动态规划解决方案**
```csharp
public int ClimbStairs(int n) {
    if (n <= 2) return n;
    
    int[] dp = new int[n + 1];
    dp[1] = 1;
    dp[2] = 2;
    
    for (int i = 3; i <= n; i++) {
        dp[i] = dp[i - 1] + dp[i - 2];
    }
    
    return dp[n];
}
```

**C# 空间优化解决方案**
```csharp
public int ClimbStairs(int n) {
    if (n <= 2) return n;
    
    int prev2 = 1, prev1 = 2;
    
    for (int i = 3; i <= n; i++) {
        int current = prev1 + prev2;
        prev2 = prev1;
        prev1 = current;
    }
    
    return prev1;
}
```

**复杂度分析**
- 时间复杂度：O(n)
- 空间复杂度：O(n)（或O(1)优化版本）

**图形讲解**
```
n = 5 时的所有路径：

                    5
          /                \
        3                    4
      /   \                /   \
    1       2            2       3
   / \     / \          / \     / \
  1   2   1   2        1   2   1   2

递推关系：
dp[1] = 1      (1)
dp[2] = 2      (1+1, 2)
dp[3] = 3      (dp[2]+dp[1] = 2+1)
dp[4] = 5      (dp[3]+dp[2] = 3+2)
dp[5] = 8      (dp[4]+dp[3] = 5+3)

可视化：
楼梯：   [n=1] [n=2] [n=3] [n=4] [n=5]
方法数：  1     2     3     5     8

对应斐波那契数列！
```

---

### 题目 6.2：打家劫舍（LeetCode 198）

**难度**：Medium | **频率**：⭐⭐⭐⭐

**题目描述**
```
你是一个专业的小偷，计划偷窃沿街的房屋。
每间房屋内都藏有一定的现金，影响你偷窃的唯一制约因素就是相邻的房屋装有相互连通的防盗系统，
如果两间相邻的房屋在同一晚被偷，系统会自动报警。

给定一个代表每个房屋存放金额的非负整数数组，确定最多能偷窃多少金额（不能偷相邻的房屋）。

示例：nums = [1,2,3,1]
输出：4 （偷第1间房屋（金额=1）和第3间房屋（金额=3）。偷得总金额 = 1 + 3 = 4）
```

**深度DP思路分析**

1. **问题转化**：
   - 这是个"选择"问题：每间房屋可偷/可不偷
   - 约束：不能偷相邻的房屋
   - 目标：最大化金额

2. **递推关系推导**：
   - 对于房屋i，有两个选择：
     - **偷房屋i**：被限制不能偷i-1，所以能带走`dp[i-2] + nums[i]`
     - **不偷房屋i**：那就是`dp[i-1]`的最优
   - 取两者更大值：`dp[i] = max(dp[i-1], dp[i-2] + nums[i])`

3. **为什么这样工作**：
   - `dp[i-1]`已经计算出前i-1间房屋的最优
   - `dp[i-2] + nums[i]`是在不影响i-1的情况下抢i
   - 两者取大就是前i间的最优

4. **常见错误**：
   - Base case设置：dp[0]=nums[0], dp[1]=max(nums[0],nums[1])
   - 边界处理很关键

**答案思路**
- **递推关系**：dp[i] = max(dp[i-1], dp[i-2]+nums[i])
- **关键洞察**：每间房是"偷/不偷"的选择问题
- **思考方式**：自bottom-up构建最优解

**C# 动态规划解决方案**
```csharp
public int Rob(int[] nums) {
    if (nums == null || nums.Length == 0) return 0;
    if (nums.Length == 1) return nums[0];
    
    int[] dp = new int[nums.Length];
    dp[0] = nums[0];
    dp[1] = Math.Max(nums[0], nums[1]);
    
    for (int i = 2; i < nums.Length; i++) {
        dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);
    }
    
    return dp[nums.Length - 1];
}
```

**C# 空间优化版本**
```csharp
public int Rob(int[] nums) {
    if (nums == null || nums.Length == 0) return 0;
    
    int prev2 = 0, prev1 = 0;
    
    foreach (int num in nums) {
        int current = Math.Max(prev1, prev2 + num);
        prev2 = prev1;
        prev1 = current;
    }
    
    return prev1;
}
```

**复杂度分析**
- 时间复杂度：O(n)
- 空间复杂度：O(n)（或O(1)优化版本）

**图形讲解**
```
nums = [1, 2, 3, 1]

决策树：
                  1        2        3        1
                偷        不偷      偷        不偷
            /              \      /          \
          偷1              1      +3          4
         /  \
      不偷2  偷2
      ...

DP表：
房屋：    [0]  [1]  [2]  [3]
金额：     1    2    3    1
dp[i]：    1    2    4    4

解释：
dp[0] = 1 (只能偷第0间)
dp[1] = 2 (偷第1间比偷第0间更优)
dp[2] = 4 (偷第0间+第2间 = 1+3)
dp[3] = 4 (不变，因为偷第3间的1不如前面)

答案：4
```

---

## 面试建议

### 准备清单
- [ ] 实现所有上述题目（C#代码）
- [ ] 能够在10-15分钟内完整讲解一个题目
- [ ] 理解时间和空间复杂度的权衡
- [ ] 学会用图形讲解算法思路

### 面试流程
1. **理解题意**（2分钟） - 确认约束条件
2. **阐述思路**（3分钟） - 讲解算法思路，不急着写代码
3. **编写代码**（5分钟） - 清晰的代码
4. **测试样例**（2分钟） - 走过几个测试用例
5. **优化讨论**（2分钟） - 讨论复杂度和优化空间

### 常见问题
**Q: 如何处理边界情况？**
A: 在编码前就列出边界情况（空输入、单个元素、重复值等）

**Q: 为什么选择这个算法？**
A: 对比其他方案的时间/空间复杂度，说明为什么这个更优

**Q: 实现有问题怎么办？**
A: 保持冷静，逐行检查代码，用样例追踪执行过程

---

## 快速参考

### LeetCode 编号速查
- 二叉树：94（中序）, 102（层序）, 145（后序）
- 链表：206（反转）, 21（合并）
- 数组字符串：1（两数之和）, 151（反转），5（回文）
- 查找排序：704（二分）, 快排（自定义）
- 设计：146（LRU）
- 动态规划：70（爬楼梯）, 198（打家劫舍）

### 复杂度对照表
| 算法 | 时间 | 空间 |
|------|------|------|
| 二叉树遍历 | O(n) | O(h) |
| 快速排序 | O(n log n) avg | O(log n) |
| 二分查找 | O(log n) | O(1) |
| LRU缓存 | O(1) | O(capacity) |
| DP（爬楼梯） | O(n) | O(n) |

---

**最后加油！祝你面试成功！** 🚀
