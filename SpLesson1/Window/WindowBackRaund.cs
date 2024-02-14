using SpLesson1.Service;
using SpLesson1.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Diagnostics;
using SpLesson1.Models;
using GalaSoft.MvvmLight.Messaging;
using System.Reflection.Metadata;

namespace SpLesson1.Window
{
    public class WindowBackRaund : NotificationService, ICommand
    {
        public List<string> BlackList { get; set; }
        public ObservableCollection<Model> Lists {  get; set; }
        
        public ICommand CreateProces { get; set; }
        public ICommand EndProces { get; set; }
        public ICommand BlackProces { get; set; }
        public Model model { get; set; }
        
        public event EventHandler? CanExecuteChanged;
        public WindowBackRaund()
        {
            CreateProces = new RelayCommand(ExecuteCreate, CanExecuteCreate);
            EndProces = new RelayCommand(Execute, CanExecute);
            BlackProces = new RelayCommand(ExecuteBlack, CanExecuteBlack);
            Lists = new();
            BlackList = new();
            Fill();
        }

        private bool CanExecuteBlack(object? obj)
        {
            return true;
        }

        private void ExecuteBlack(object? obj)
        {
            BlackList.Add((string)obj);
        }

        public bool CanExecuteCreate(object? parameter)
        {
            return true;
        }

        public void ExecuteCreate(object? parameter)
        {
            foreach (var item in BlackList)
            {
                if (item==(string)parameter)
                {
                    return;
                }
            }      
            Process.Start((string)parameter);
            Fill();
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            int ss=(int)parameter;
            int id = Lists[ss].Id;
            var pro = Process.GetProcessById(id);
            pro.Kill();
            Fill();
        }

        public void Fill()
        {
            Lists.Clear();
            var AllGetProcesses = Process.GetProcesses().ToList();
            foreach (var item in AllGetProcesses)
            {
                model = new();
                model.Id = item.Id;
                model.Name = item.ProcessName;
                model.MachineName = item.MachineName;
                Lists.Add(model);

            }
        }
    }
}
