# Item ListView
***
Custom ListView for .NET WinForms to replicate functionality of listviews available on mobile devices, especially Android.

## USAGE

**Create a class representing your entity**
```csharp
public class Person
{
    public string CNIC { get; set; }
    public string Name { get; set; }
}
```

**Create a `UserControl` to represent your ListView Item on UI and add relevant methods**
![User Control][usercontrol]
```csharp
public partial class PersonControl : UserControl
{
    public PersonControl()
    {
        InitializeComponent();
    }

    public string PName
    {
        set 
        {
            PersonName.Text = value;
        }
    }

    public string Cnic
    {
        set 
        {
            CNIC.Text = value;
        }
    }

    public bool Active 
    {
        set 
        {
            SelectionIndicator.Visible = value;
        }
    }
}
```

**Optionally create a `UserControl` to represent your ListView's header**
![Header User Control][headerusercontrol]
`No code would be required in this case as it is just a header`

**Create an adapter, inherit it from `ListAdapter` and implement the `Draw` method to tell ItemListView how to draw the items on screen**
```csharp
public class PersonAdapter : ListAdapter<PersonControl, Person>
{
    public PersonAdapter(List<Person> items, PersonControlHeader header) : base(items, header)
    {
    }

    public override PersonControl Draw(PersonControl control, Person item)
    {
        control.PName = item.Name;
        control.Cnic = item.CNIC;

        return control;
    }
}
```
> Note that we are passing header element to the base as well, but this is not necessary. A header is different from other list items as it is `non-interactable`

**Finally, use the list view in your Windows Form**
![mainForm][mainForm]
```csharp
public partial class Form1 : Form
{
    List<Person> person;
    PersonAdapter adapter;
    public Form1()
    {
        InitializeComponent();
        person = new List<Person>();
        person.AddRange(new Person[]
        {
            new Person 
            {
                CNIC = "XXXXX-XXXXXXX-X",
                Name = "Person A"
            },
            new Person
            {
                CNIC = "XXXXX-XXXXXXX-X",
                Name = "Person B"
            },
            new Person
            {
                CNIC = "XXXXX-XXXXXXX-X",
                Name = "Person C"
            }
        });
        adapter = new PersonAdapter(person, new PersonControlHeader());
        // Set adapter to the itemListView, so basically the adapter later on controls what is being displayed on screen
        itemListView1.SetAdapter(adapter);
    }
    
    private void Add_Click(object sender, EventArgs e)
    {
        person.Add(new Person 
        {
            CNIC = CNICTxtBox.Text,
            Name = NameTxtBox.Text
        });
        adapter.NotifyDataSetChanged();
    }

    private void Delete_Click(object sender, EventArgs e)
    {
        if (itemListView1.SelectedIndex != -1)
        {
            person.RemoveAt(itemListView1.SelectedIndex);
            adapter.NotifyDataSetChanged();
        }
        else
        {
            MessageBox.Show("You must select an index to remove");
        }
    }
}
```

## Optional
- You can also attach a `ItemClickListener` to the listitems
```csharp
itemListView1.SetOnItemClickListener(OnItemClickListener);

public object OnItemClickListener(object sender, OnItemClickEventArgs<Control> e)
{
    ResetAll();
    ((PersonControl)e.Item).Active = true;
    ((PersonControl)e.Item).BackColor = Color.AliceBlue;
    return null;
}

private void ResetAll()
{
    foreach (var control in this.itemListView1.ListElements)
    {
        if (control.GetType() == typeof(PersonControl)) 
        { 
            ((PersonControl)control).Active = false;
            ((PersonControl)control).BackColor = Color.White;
        }
    }
}
```

- A `SelectedIndexChanged` trigger can also be set to the listview
```
itemListView1.SelectedIndexChanged += ItemListView1_SelectedIndexChanged;

private void ItemListView1_SelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
{
    MessageBox.Show(e.Index + "");
}
```

Checkout the code for the demo application [here](https://github.com/ShujaARizvi/ItemListView-Demo-App).

[usercontrol]: https://user-images.githubusercontent.com/45180820/87250605-6ace0a00-c47f-11ea-8b75-aad6ad5c7c46.png "User Control"
[headerusercontrol]: https://user-images.githubusercontent.com/45180820/87250779-c0ef7d00-c480-11ea-9395-181103f2f139.png
[mainForm]: https://user-images.githubusercontent.com/45180820/87251024-3445be80-c482-11ea-8a0d-af1218e6e144.png