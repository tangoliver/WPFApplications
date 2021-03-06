﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using PDMTools.defined;
using PDMTools.datas;

namespace PDMTools.models
{
    /*
     * 管理日志数据及对应UI显示
     */
    public class LogModel : BaseModel
    {
        private TextBox mLogTextBox = null;

        public override void init(MainWindow win)
        {
            base.init(win);
            mLogTextBox = win.LogTxt;
            showState(Defined.UiState.Idle);
        }

        public override void deinit()
        {
            mLogTextBox = null;
            base.deinit();
        }

        public override void showState(Defined.UiState state)
        {
            if (!isInited())
            {
                return;
            }

            switch (state)
            {
                case Defined.UiState.SelectedFirmware:
                case Defined.UiState.SelectedTemplate:
                case Defined.UiState.SelectedTool:
                case Defined.UiState.SelectedFirmwareAndTool:
                case Defined.UiState.Doing:
                    {
                        mLogTextBox.IsEnabled = true;
                        mLogTextBox.IsReadOnly = true;
                    }
                    break;

                case Defined.UiState.Idle:
                default:
                    {
                        mLogTextBox.IsEnabled = false;
                        mLogTextBox.IsReadOnly = true;
                    }
                    break;
            }
        }

        public override bool isValid(Defined.UiState state)
        {
            if (!isInited())
            {
                return false;
            }

            return true;
        }

        public override List<Operate> getOperates(Defined.UiState state)
        {
            return null;
        }

        public void print(string line)
        {
            if (!isInited())
            {
                return;
            }
            
            // async print
            mWin.Dispatcher.BeginInvoke(new Action(()
                =>
                {
                    mLogTextBox.Text += (DateTime.Now.ToString("[hh:mm:ss] ")
                        + line + Environment.NewLine);
                    mLogTextBox.ScrollToEnd();
                }));
        }

        public void clear()
        {
            if (!isInited())
            {
                return;
            }

            // async clear
            mWin.Dispatcher.BeginInvoke(new Action(()
                => mLogTextBox.Text = ""));
        }
    }
}
