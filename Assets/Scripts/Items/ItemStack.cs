using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack {
    public Item Item { get; private set; }
    public int Count { get; private set; }
    public readonly int MaxSize;

    public bool IsEmpty {
        get {
            return Count <= 0;
        }
    }

    public ItemStack(Item item, int count = 1) {
        Item = item;
        Count = count;
        MaxSize = item.StackSize;
    }


    public static ItemStack operator +(ItemStack left, ItemStack right) {
        left.Count += right.Count;
        return left;
    }

    public static ItemStack operator -(ItemStack left, int right) {
        left.Count -= right;
        if (left.Count < 0)
            left.Count = 0;
        return left;
    }

    public bool Contains(Item item) {
        return Item == item;
    }

    public bool StacksWith(ItemStack stack) {
        return Item == stack.Item;
    }

    public static bool operator <(ItemStack left, ItemStack right) {
        return left.Count < right.Count;
    }
    public static bool operator <=(ItemStack left, ItemStack right) {
        return left.Count <= right.Count;
    }
    public static bool operator >(ItemStack left, ItemStack right) {
        return left.Count > right.Count;
    }
    public static bool operator >=(ItemStack left, ItemStack right) {
        return left.Count >= right.Count;
    }

    public static bool operator <(ItemStack left, int right) {
        return left.Count < right;
    }
    public static bool operator <=(ItemStack left, int right) {
        return left.Count <= right;
    }
    public static bool operator >(ItemStack left, int right) {
        return left.Count > right;
    }
    public static bool operator >=(ItemStack left, int right) {
        return left.Count >= right;
    }

    public override string ToString() {
        return "[" + Count + "]: " + Item + "\n";
    }
}
