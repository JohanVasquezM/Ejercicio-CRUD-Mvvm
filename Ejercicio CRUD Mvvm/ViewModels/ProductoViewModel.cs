using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using Ejercicio_CRUD_Mvvm.Models;
using Ejercicio_CRUD_Mvvm.Data;
using System.Threading.Tasks;

namespace Ejercicio_CRUD_Mvvm.ViewModels
{
    public class ProductoViewModel : BindableObject
    {
        private Database _database;
        private Producto _producto;

        public ObservableCollection<Producto> Productos { get; set; }

        public Producto Producto
        {
            get { return _producto; }
            set
            {
                _producto = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; }

        public ProductoViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EjercicioCRUDMvvm.db3");
            _database = new Database(dbPath);
            Productos = new ObservableCollection<Producto>();
            Producto = new Producto();
            SaveCommand = new Command(OnSave);

            LoadProductos();
        }
        private async void LoadProductos()
        {
            var productos = await _database.GetProductosAsync();
            foreach (var producto in productos)
            {
                Productos.Add(producto);
            }
        }
        private async void OnSave()
        {
            if (string.IsNullOrWhiteSpace(Producto.Nombre) ||
                string.IsNullOrWhiteSpace(Producto.Descripcion) ||
                Producto.Precio <= 0 ||
                Producto.Stock <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "Entendido");
                return;
            }

            await _database.SaveProductoAsync(Producto);
            Productos.Add(Producto);
            Producto = new Producto();
        }
    }
}

