using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;

namespace Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1: Find Missing Numbers in Array
            Console.WriteLine("Question 1:");
            int[] nums1 = { 4, 3, 2, 7, 8, 2, 3, 1 };
            IList<int> missingNumbers = FindMissingNumbers(nums1);
            Console.WriteLine(string.Join(",", missingNumbers));

            // Question 2: Sort Array by Parity
            Console.WriteLine("Question 2:");
            int[] nums2 = { 3, 1, 2, 4 };
            int[] sortedArray = SortArrayByParity(nums2);
            Console.WriteLine(string.Join(",", sortedArray));

            // Question 3: Two Sum
            Console.WriteLine("Question 3:");
            int[] nums3 = { 2, 7, 11, 15 };
            int target = 9;
            int[] indices = TwoSum(nums3, target);
            Console.WriteLine(string.Join(",", indices));

            // Question 4: Find Maximum Product of Three Numbers
            Console.WriteLine("Question 4:");
            int[] nums4 = { 1, 2, 3, 4 };
            int maxProduct = MaximumProduct(nums4);
            Console.WriteLine(maxProduct);

            // Question 5: Decimal to Binary Conversion
            Console.WriteLine("Question 5:");
            int decimalNumber = 42;
            string binary = DecimalToBinary(decimalNumber);
            Console.WriteLine(binary);

            // Question 6: Find Minimum in Rotated Sorted Array
            Console.WriteLine("Question 6:");
            int[] nums5 = { 3, 4, 5, 1, 2 };
            int minElement = FindMin(nums5);
            Console.WriteLine(minElement);

            // Question 7: Palindrome Number
            Console.WriteLine("Question 7:");
            int palindromeNumber = 121;
            bool isPalindrome = IsPalindrome(palindromeNumber);
            Console.WriteLine(isPalindrome);

            // Question 8: Fibonacci Number
            Console.WriteLine("Question 8:");
            int n = 4;
            int fibonacciNumber = Fibonacci(n);
            Console.WriteLine(fibonacciNumber);
        }

        /* 
         * Question 1: Find Missing Numbers in Array
         * Description:
         *   Given an unsorted integer array containing numbers from 1 to n (with possible duplicates),
         *   this function finds all the numbers in the range [1, n] that do not appear in the array.
         *
         * Approach:
         *   - Use a negative marking technique: iterate through the array and for each number x,
         *     mark the element at index x-1 as negative.
         *   - Then, scan the array; any index i with a positive value means that (i+1) is missing.
         *
         * Edge Cases & Examples:
         *   - Empty array: return an empty list.
         *         Example: Input: []          => Output: []
         *   - Complete array (no missing numbers): return an empty list.
         *         Example: Input: [1,2,3,4,5]  => Output: []
         *   - Improper input: if the maximum value in the array is greater than the length of the array,
         *     throw an ArgumentException.
         *         Example: Input: [4,3,2,7,8,2,2,3,10] => Exception thrown.
         *   - Example (normal case):
         *         Input: [4,3,2,7,8,2,2,3,1] => Output: [5, 6]
         */
        public static IList<int> FindMissingNumbers(int[] nums)
        {
            if (nums == null)
            {
                throw new ArgumentNullException("Input array is null");
            }
            int maxValueinInpt = nums.Max();
            if (maxValueinInpt > nums.Length)
            {
                throw new ArgumentException("Improper Input : Max Value is in the input is more than the length of the array");
            }
            try
            {
                
                List<int> missingNumbers = new List<int>();

                if (nums.Length == 0)
                {
                    return missingNumbers; // Early termination for empty array.
                }
                // Negative marking technique.
                for (int i = 0; i < nums.Length; i++)
                {
                    int currentIndex = Math.Abs(nums[i]) - 1;
                    if (nums[currentIndex] > 0)
                    {
                        nums[currentIndex] = -nums[currentIndex];
                    }
                }
                // Identify missing numbers by checking for positive values.
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] > 0)
                    {
                        missingNumbers.Add(i + 1);
                    }
                }
                return missingNumbers;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentException in FindMissingNumbers: " + ex.Message);
                throw;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("IndexOutOfRangeException in FindMissingNumbers: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in FindMissingNumbers: " + ex.Message);
                throw;
            }
        }

        /* 
         * Question 2: Sort Array by Parity
         * Description:
         *   Rearrange the given integer array such that all even integers appear at the beginning,
         *   followed by all odd integers.
         *   
         * Assumption:
         *   In both the given examples, the orginial relative order is preserved, hence I am taking a nested loop approach based on this assumption.
         *   But if this is not the criteria, we can even solve the same problem with two pointer approach and solve in single loop.
         *   
         * Approach:
         *   - Use a stable in-place algorithm:
         *       1. Maintain an insert position starting at index 0.
         *       2. Iterate through the array; when an even number is found,
         *          shift all elements between the insert position and the current index one step to the right,
         *          then place the even number at the insert position.
         *       3. Increment the insert position.
         *
         * Edge Cases & Examples:
         *   - Empty array: returns an empty array.
         *         Example: Input: []                => Output: []
         *   - Single element:
         *         Example: Input: [2] (even)         => Output: [2]
         *                  Input: [3] (odd)          => Output: [3]
         *   - All even array: 
         *         Example: Input: [2,4,6,8]         => Output: [2,4,6,8]
         *   - All odd array: 
         *         Example: Input: [1,3,5,7]         => Output: [1,3,5,7]
         *   - Mixed with zero:
         *         Example: Input: [0,1,2]           => Output: [0,2,1]
         *   - Negative numbers:
         *         Example: Input: [-3,-2,-1,-4,-5]  => Output: [-2,-4,-3,-1,-5]
         *   - Duplicates:
         *         Example: Input: [2,3,2,1,4,3]     => Output: [2,2,4,3,1,3]
         */
        public static int[] SortArrayByParity(int[] nums)
        {
            try
            {

                if (nums == null)
                {
                    throw new ArgumentNullException("Input array is null");
                }

                if (nums.Length == 0)
                {
                    return nums; // Early termination for empty array.
                }

                int insertPosition = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] % 2 == 0) // Check for even number
                    {
                        int evenValue = nums[i];

                        // Shift elements from insertPosition to i one position to the right.
                        for (int j = i; j > insertPosition; j--)
                        {
                            nums[j] = nums[j - 1];
                        }
                        nums[insertPosition] = evenValue;
                        insertPosition++;
                    }
                }
                return nums;

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("ArgumentNullException in SortArrayByParity: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SortArrayByParity: " + ex.Message);
                throw;
            }
        }

        /* 
         * Question 3: Two Sum
         * Description:
         *   Given an array of integers and a target value, return the indices of the two numbers that add up to the target.
         *
         * Approach:
         *   - Use a dictionary (hash map) to store the numbers and their indices.
         *   - Iterate through the array, and for each number, check if the complement (target - number)
         *     exists in the dictionary.
         *   - If found, return the indices; otherwise, add the number and its index to the dictionary.
         *
         * Edge Cases & Examples:
         *   - Negative numbers:
         *         Example: Input: [-3,4,3,90], target = 0      => Output: [0, 2]
         *   - Duplicate numbers:
         *         Example: Input: [3,3], target = 6              => Output: [0, 1]
         *   - Empty array:
         *         Example: Input: [] with any target             => Output: []
         *   - Normal case:
         *         Example: Input: [2,7,11,15], target = 9          => Output: [0, 1]
         */
        public static int[] TwoSum(int[] nums, int target)
        {
            try
            {

                if (nums == null)
                {
                    throw new ArgumentNullException("Input array is null");
                }
                if (nums.Length == 0)
                {
                    return new int[] { }; // Early termination for empty array.
                }
                Dictionary<int, int> map = new Dictionary<int, int>();
                for (int i = 0; i < nums.Length; i++)
                {
                    int complement = target - nums[i];
                    if (map.ContainsKey(complement))
                    {
                        return new int[] { map[complement], i };
                    }
                    if (!map.ContainsKey(nums[i]))
                    {
                        map[nums[i]] = i;
                    }
                }
                Console.WriteLine("No Two sum solution");
                return new int[] { };
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("ArgumentNullException in TwoSum: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in TwoSum: " + ex.Message);
                throw;
            }
        }

        /* 
        * Question 4: Find Maximum Product of Three Numbers
        * Description:
        *   Given an integer array, find three numbers whose product is maximum and return that product.
        *
        * Approach:
        *   - Sort the array.
        *   - The maximum product is the greater of:
        *         (i) The product of the three largest numbers.
        *        (ii) The product of the two smallest (possibly negative) numbers and the largest number.
        *
        * Edge Cases & Examples:
        *   - Array length less than 3: throw an exception.
        *         Example: Input: [1,2]         => Exception thrown.
        *   - All negatives:
        *         Example: Input: [-1,-2,-3]      => Output: -6
        *   - Normal case:
        *         Example: Input: [1,2,3,4]       => Output: 24
        */
        public static int MaximumProduct(int[] nums)
        {
            if (nums == null)
            {
                throw new ArgumentNullException("Input array is null");
            }
            if (nums.Length < 3)
            {
                throw new ArgumentException("Input array must have at least 3 numbers.");
            }
  
            try
            {
                
                if (nums.Length == 0)
                {
                    return 0;
                }
                Array.Sort(nums);
                int n = nums.Length;

                int product1 = nums[n - 1] * nums[n - 2] * nums[n - 3];
                int product2 = nums[0] * nums[1] * nums[n - 1];

                return Math.Max(product1, product2);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("ArgumentNullException in MaximumProduct: " + ex.Message);
                throw;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentException in MaximumProduct: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in MaximumProduct: " + ex.Message);
                throw;
            }
        }

        /* 
         * Question 5: Decimal to Binary Conversion
         * Description:
         *   Convert a given decimal number to its binary representation.
         *
         * Assumption:
         *   Using a sign magnitude to handle negative numbers. Generally negative numbers are represented with 2's compliment in binary.
         *   But the input parameter's max value is not given , and hence it is hard for me to determine the number of bits required.
         *
         * Approach:
         *   - If the number is 0, return "0".
         *   - For a non-zero number, repeatedly divide by 2 and prepend the remainder to form the binary string.
         *   - For negative numbers, use sign-magnitude representation by converting the absolute value and
         *     prefixing the result with a "-" sign.
         *
         * Edge Cases & Examples:
         *   - Input 0:
         *         Example: Input: 0              => Output: "0"
         *   - Positive number:
         *         Example: Input: 42             => Output: "101010"
         *   - Negative number:
         *         Example: Input: -5             => Output: "-101"
         */
        public static string DecimalToBinary(int decimalNumber)
        {
            try
            {
                

                if (decimalNumber == 0)
                {
                    return "0";
                }
                bool isNegative = decimalNumber < 0;
                if (isNegative)
                {
                    decimalNumber = -decimalNumber;
                }
                string binaryNumber = "";
                while (decimalNumber > 0)
                {
                    binaryNumber = (decimalNumber % 2).ToString() + binaryNumber;
                    decimalNumber /= 2;
                }
                return isNegative ? "-" + binaryNumber : binaryNumber;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DecimalToBinary: " + ex.Message);
                throw;
            }
        }

        /* 
          * Question 6: Find Minimum in Rotated Sorted Array
          * Description:
          *   Given a rotated sorted array (with duplicate elements), find the minimum element.
          *
          * Approach:
          *   - Use a modified binary search.
          *   - Compare the middle element with the rightmost element to decide whether the minimum
          *     is in the left or right half.
          *
          * Edge Cases & Examples:
          *   - Array that is not rotated:
          *         Example: Input: [1,2,3,4,5]   => Output: 1
          *   - Single element array:
          *         Example: Input: [10]          => Output: 10
          *   - Empty array: throw an exception.
          *         Example: Input: []            => Exception thrown.
          *   - Normal case:
          *         Example: Input: [3,4,5,1,2]     => Output: 1
          *   - Edge case:
          *         Example: Input: [ 6,7,8,1,2,3,4,4,5]      => Output: 1
          */
        public static int FindMin(int[] nums)
        {
            if (nums == null)
            {
                throw new ArgumentNullException("Input array is null");
            }
            if (nums.Length == 0)
            {
                throw new ArgumentException("Empty Array Given");
            }
            try
            {
                
                int left = 0;
                int right = nums.Length - 1;

                while (left < right)
                {
                    int mid = left + (right - left) / 2;
                    // If mid element is greater than the right most element, the minimum must be in the right half
                    if (nums[mid] > nums[right])
                    {
                        left = mid + 1;
                    }
                    else if (nums[mid] < nums[right])
                    {
                        // else the minimum is the left half
                        right = mid;
                    }
                    else
                    {
                        right--;
                    }
                }
                return nums[left];

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("ArgumentNullException in FindMin: " + ex.Message);
                throw;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentException in FindMin: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in FindMin: " + ex.Message);
                throw;
            }
        }

        /* 
         * Question 7: Palindrome Number
         * Description:
         *   Determine whether an integer is a palindrome. An integer is a palindrome if it reads the same forward and backward.
         *
         * Approach:
         *   - Negative numbers are not palindromes.
         *   - Also, if a number ends in 0 (and is not 0), it cannot be a palindrome.
         *   - Reverse half of the number and compare with the remaining half. For odd-length numbers,
         *     discard the middle digit by dividing the reversed half by 10.
         *
         * Edge Cases & Examples:
         *   - Negative numbers:
         *         Example: Input: -121           => Output: false
         *   - Numbers ending with 0 (except 0):
         *         Example: Input: 10             => Output: false
         *   - Zero:
         *         Example: Input: 0              => Output: true
         *   - Normal case:
         *         Example: Input: 121            => Output: true
         */
        public static bool IsPalindrome(int x)
        {
            try
            {
                
                if ( x < 0  ||( x %10 == 0 && x != 0))
                {
                    return false;
                }
                int reverse = 0;
                
                while(x > reverse)
                {
                    reverse = reverse * 10 + x % 10;
                    x /= 10;
                }
                return x == reverse || x == reverse / 10;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /* 
         * Question 8: Fibonacci Number
         * Description:
         *   Calculate the nth Fibonacci number where:
         *       F(0) = 0, F(1) = 1, and F(n) = F(n-1) + F(n-2) for n > 1.
         *
         * Approach:
         *   - Use an iterative approach to compute Fibonacci numbers.
         *   - Start with base cases for n = 0 and n = 1, then iterate up to n.
         *
         * Edge Cases & Examples:
         *   - n = 0:
         *         Example: Input: 0              => Output: 0
         *   - n = 1:
         *         Example: Input: 1              => Output: 1
         *   - n = 4:
         *         Example: Input: 4              => Output: 3
         *   - n = 30 (boundary test):
         *         Example: Input: 30             => Output: 832040
         */
        public static int Fibonacci(int n)
        {
            try
            {
                // base cases
                if (n == 0) return 0;
                if (n == 1) return 1;

                int a = 0;
                int b = 1;

                for (int i = 2; i <= n; i++)
                {
                    int c = a + b;
                    a = b;
                    b = c;
                }
                return b;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
