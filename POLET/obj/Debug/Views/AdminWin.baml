   M S B A M L   `   `   `  ����@  <POLET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:POLET.ViewsPOLET.Views  5   	       POLET.Views.AdminWin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:POLET.Views     5   	     � Title$+  %Окно администратора��5   	   $
��1080��6%   $
��1920��63    � WindowState$ 	Maximized=�6@    � 
ResizeMode$ NoResize=�6X   ���	#FF85DCE2.��5	      � .+�5
   
   �� -   5         ��Name$ 
ExitButton��5
   X   ���	#FF85DCE2$��19(�6(   $��../Fonts/#Montserrat)�66   $��
Выход��6l   $
��Left=�6|   $	��Top=�6�   $	��149��6�   $��36��6�   $��	16,16,0,0q�6�   6�   �� 5   
   $#��../Fonts/#Montserrat SemiBold)�6   $��Center=�6;   $��95(�6X   $��	0,0,0,890q�6f   .��6y   
ПОЛЁТ6�   �� 5   
   $��../Fonts/#Montserrat)�6   $��Center=�62   $��26(�6O   $��
0,94,0,895q�6]   .��6q   -+комната администратора6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ FlightsDataGrid��5   *      CanUserAddRows .     AutoGenerateColumns .  $��0,174,0,185q�6   ���	#FF85DCE2   
IsReadOnly . $��20(�6�   $��./Fonts/#Montserrat)�6�   ��6C   �� *	flights+ � ContextMenu 5      �� 5      .@�5      m� -   5        	 m�Name$	 AddMenu��5   L   $��Добавить рейс��6   6L   m� 5      -   5        $	 Delete��5   J   $��Удалить рейс��6   6J   5      
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Номер рейса��6%      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *
idflight+6W     5      $# Свободные места��6%    6>   �� *numberseats+  5      $ Откуда��6%    65   �� *
fromflight+  5      $ Куда��6%    63   �� *whereflight+  5      $% Дата отправления��6%    6?   �� *
datefrom+ �� StringFormat$ {0:dd.MM.yyyy}��  5      $' !Время отправления��6%    6@   �� *
timefrom+  5      $ Дата прибытия��6%    6<   �� *	datewhere+$ {0:dd.MM.yyyy}��  5      $! Время прибытия��6%    6=   �� *	timewhere+  5      $ Пересадки��6%    68   �� *Transfers.namecity+  5      $ Цена��6%    63   �� *price+5!      �� 5"   
   -   5        $ AddButon��5"   `   ���	#FFFFBE49$��28(�6(   $"��./Fonts/#Montserrat SemiBold)�66   $��Добавить рейс��6r   $
��Left=�6�   $	��Top=�6�   $	��282��6�   $��50��6�   $��238,953,0,0q�6�   6�   �� 5#   
   -   5        $ DeleteButton��5#   `   ���	#FFFFBE49$��28(�6(   $"��./Fonts/#Montserrat SemiBold)�66   $��Удалить рейс��6v   $
��Left=�6�   $	��Top=�6�   $	��282��6�   $��50��6�   $��1390,953,0,0q�6�   6�   �� 5$   
   -   5        $ 
EditButton��5$   `   ���	#FFFFBE49$��28(�6(   $"��./Fonts/#Montserrat SemiBold)�66   $��Изменить рейс��6t   $��Center=�6�   $	��Top=�6�   $	��282��6�   $��50��6�   $��	0,953,0,0q�6�   6�   5%      5&      