using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    ItemStack[] contents;

    public int Size { get; private set; }
    public int Count { get; private set; }


    public Inventory(int size) {
        Size = size;
        contents = new ItemStack[size];
    }

    // Give

    public bool Give(ItemStack stack, int index) {
        if (index < 0 || index >= contents.Length) {
            return false;
        }
        if (stack == contents[index]) {
            contents[index] += stack;
            return true;
        } else {
            return false;
        }
    }

    public bool Give(ItemStack stack) {
        bool given = false;

        // Check if stack can be combined with existing stack
        for (int n = 0; n < Size; n++) {
            if (contents[n] != null) {
                if (contents[n].StacksWith(stack)) {
                    contents[n] += stack;
                    given = true;
                    break;
                }
            }
        }
        if (given) {
            return given;
        }

        // Place in first null slot
        for (int n = 0; n < Size; n++) {
            if (contents[n] == null) {
                contents[n] = stack;
                given = true;
                break;
            }
        }
        return given;
    }
    // Take
    // public ItemStack Pull(int index, int count = 1) {
    //     if(contents[index] != null) {
    //         return null;
    //     }
    //     ItemStack stack = new ItemStack(item, count);
    //     for (int n = 0; n < Size; n++) {
    //         if (contents[n] != null) {
    //         }
    //     }
    //     return null;
    // }

    public ItemStack Pull(int count, int index) {
        if (index < 0 || index >= contents.Length) {
            return null;
        }
        ItemStack stack = contents[index];
        if (stack == null) {
            return null;
        }
        if (count < 0 || count >= stack.Count) {
            return null;
        }
        int retCount = Mathf.Min(count, stack.Count);
        ItemStack retStack = new ItemStack(stack.Item, retCount);
        contents[index] -= retCount;
        if (contents[index] <= 0) {
            contents[index] = null;
        }
        return retStack;
    }
    public ItemStack Pull(int count = 1) {
        if (IsEmpty()) {
            return null;
        }
        int pullIndex = 0;
        for (int n = contents.Length - 1; n >= 0; n--) {
            if (contents[n] != null) { 
                pullIndex = n;
            }
        }
        int retCount = Mathf.Min(count, contents[pullIndex].Count);
        ItemStack retStack = new ItemStack(contents[pullIndex].Item, retCount);
        contents[pullIndex] -= retCount;
        if (contents[pullIndex].IsEmpty) {
            contents[pullIndex] = null;
        }
        return retStack;




    }

    public ItemStack PullStack(int index) {
        if (index < 0 || index >= contents.Length) {
            return null;
        }
        ItemStack retStack = contents[index];
        contents[index] = null;
        return retStack;
    }

    // public int OpenIndex() {
    //     for (int n = 0; n < Size; n++) {
    //         if (contents[n] == null) {
    //             return n;
    //         }
    //     }
    //     return -1;
    // }

    public bool IsEmpty() {
        foreach(ItemStack stack in contents) {
            if (stack != null) {
                return false;
            }
        }
        return true;
    }
    // public ItemStack Last() {
    //     for (int n = Size - 1; n <= 0; n--) {
    //         if (contents[n] == null) {
    //             return contents[n];
    //         }
    //     }
    //     return null;

    // }

    public override string ToString() {
        string retString = "";
        foreach (ItemStack stack in contents) {
            if (stack != null) {
                retString += stack.ToString();
            }
        }
        return retString;
    }
}
