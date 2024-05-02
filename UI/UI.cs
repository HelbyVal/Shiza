using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public partial class UI : Control
{
	// Called when the node enters the scene tree for the first time.

	List<ItemSlot> itemSlots;
	public Sprite2D sprite;
	public override void _Ready()
	{
		GetSavedItems();
		sprite = GetNode<Sprite2D>("Ui");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GetSavedItems()
	{
		itemSlots = new List<ItemSlot>()
		{
			new ItemSlot() { SlotNum = 0, Pos = new Vector2(225,980)  },
            new ItemSlot() { SlotNum = 1, Pos = new Vector2(475,980)  },
            new ItemSlot() { SlotNum = 2, Pos = new Vector2(725, 980)  },
			new ItemSlot() { SlotNum = 3, Pos = new Vector2(975, 980)  },
            new ItemSlot() { SlotNum = 4, Pos = new Vector2(980,1225)  },
            new ItemSlot() { SlotNum = 5, Pos = new Vector2(1475, 980)  }
        };
	} 

	public bool InsertItem(BaseItem item)
	{
		ItemSlot freeSlot = itemSlots.FirstOrDefault(s => s.Item == null);
		if (freeSlot != null)
		{
			freeSlot.Item = item;
			item.Position = freeSlot.Pos;
			GD.Print(item.Position.ToString());
			return true;
		}
		return false;
	}

}

	public class ItemSlot
{
	public int SlotNum;
	public BaseItem Item;
	public Vector2 Pos;
}
