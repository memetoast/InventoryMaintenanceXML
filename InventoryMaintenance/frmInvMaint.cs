using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMaintenance
{
    public partial class frmInvMaint : Form
	{
		public frmInvMaint()
		{
			InitializeComponent();
		}

		private List<InvItem> invItems = null;

		private void frmInvMaint_Load(object sender, EventArgs e)
		{
			invItems = InvItemDB.GetItems();
			FillItemListBox();
		}

		private void FillItemListBox()
		{
			lstItems.Items.Clear();
			foreach (InvItem i in invItems)
			{
				lstItems.Items.Add(i.GetDisplayText());
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			frmNewItem newItemForm = new frmNewItem();
			InvItem invItem = newItemForm.GetNewItem();
			if (invItem != null)
			{
				invItems.Add(invItem);
				InvItemDB.SaveItems(invItems);
				FillItemListBox();
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			int i = lstItems.SelectedIndex;
			if (i != -1)
			{
				InvItem invItem = (InvItem)invItems[i];
				string message = "Are you sure you want to delete "
					+ invItem.Description + "?";
				DialogResult button =
					MessageBox.Show(message, "Confirm Delete",
					MessageBoxButtons.YesNo);
				if (button == DialogResult.Yes)
				{
					invItems.Remove(invItem);
					InvItemDB.SaveItems(invItems);
					FillItemListBox();
				}
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}