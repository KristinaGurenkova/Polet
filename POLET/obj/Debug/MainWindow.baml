   M S B A M L   `   `   `  ����@  <POLET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:POLETPOLET  5   	       POLET.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:POLET     5   	     � Title$  
ПОЛЁТ��5   	   $
��1080��6   $
��1920��6%    � WindowState$ 	Maximized=�62    � 
ResizeMode$ NoResize=�6J   ���	#FF85DCE2.��5	      �  � RowDefinitions 5
   
   �� 5      $
��472* �6   �� 5      $
��593* �6   .+�5   
   �� -   5         ��Name$ AdminLoginButton��5   W   ���	#FF85DCE2$��19(�6(   $��./Fonts/#Montserrat)�66   $0��*Вход для администатора��6q   $
��Left=�6�   $	��Top=�6�   $	��256��6�   $��36��6�   $��	10,15,0,0q�6�   6�   �� 5   
   $"��./Fonts/#Montserrat SemiBold)�6   $��Center=�6:   $��95(�6W   $��2��6e   .��6v   
ПОЛЁТ6}   �� 5   
   $��./Fonts/#Montserrat)�6   $��Center=�61   $
��20.2(�6N   $��0,94,0,0q�6^   $��2��6p   .��6�   75поиск и бронирование билетов6�   �� 5   
   $��./Fonts/#Montserrat)�6   $��28(�61   $��Откуда:��6?   $
��Left=�6Q   $��280,138,0,0q�6l   $	��Top=�6�   �� 5   
   -   5         ��Name$ WhereFromBox��5   B   $��./Fonts/#Montserrat)�6   $��30(�63   $
��Left=�6X   $��38��6s   $��441,141,0,0q�6    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��282��6�   ���	#FF707070.��6�    � 	Resources 5       �� 
TargetType	 �� Property
 �� Value��%^   '��       �� 5      " ��6   .�5      �� 	 �@��CornerRadius$
 9��65   5      5      .��6!   5      �� 5   
   $��./Fonts/#Montserrat)�6   $��28(�61   $��	Куда:��6?   $
��Left=�6O   $��280,202,0,0q�6j   $	��Top=�6   �� 5   
   -   5        $ 
WhereToBox��5   B   $��./Fonts/#Montserrat)�6   $��30(�63   $
��Left=�6V   $��38��6q   $��441,205,0,0q�6}   $
 Wrap=�6�   $	��Top=�6�   $	��282��6�   ���	#FF707070.��6�    5      ��%^   '��       �� 5      " ��6   .�5      �� 	 �@��CornerRadius$
 9��65   5      5      .��6!   5       �� 5!   
   $��./Fonts/#Montserrat)�6   $��28(�61   $*��$Количество билетов:��6?   $
��Left=�6]   $��280,266,0,0q�6x   $	��Top=�6�   �� 5"   
    �� Orientation$ 
Horizontal=�6   $��603,248,1175,142q�6.   .+�5#      �� -   5        $ NumericTextBox��5#   7   $��./Fonts/#Montserrat)�6   $��1��6O   $��27(�6X   $��50��6f   $��35��6q    �� TextAlignment$ Center=�6}   ���	#FF707070.��6�    5$      ��%^   '��       �� 5%      " ��6   .�5&      �� 	 �@��CornerRadius$
 9��69   5'      5(      .��6%   5)      �� 5*      -   5        $	��▲��5*      $��35��6!   $��35��6,   ���	#FF7070706U   �� 5+      -   5        $	��▼��5+      $��35��6!   $��35��6,   ���	#FF7070706U   5,      �� 5-   
   $��./Fonts/#Montserrat)�6   $��28(�61   $��Дата вылета:��6?   $
��Left=�6V   $��280,331,0,0q�6q   $	��Top=�6�   (  "System.Windows.Controls.DatePicker  5.   
   -   5          Name$ 
DatePicker��5.      $��../Fonts/#Montserrat)�6)   $��23(�6q   $��512,336,1192,535q�6      IsTodayHighlighted .  ���	#FF85DCE2$��2��6�      SelectedDate 6K   �� *SelectedDate+6�   �� 5/   
   -   5        $ SearchButton��5/   `   ���	#FFFFBE49$��28(�6(   $"��./Fonts/#Montserrat SemiBold)�66   $��
Найти��6v   $
��Left=�6�   $	��Top=�6�   $	��157��6�   $��50��6�   $��280,384,0,0q�6�   6�   �� 50   
   $��./Fonts/#Montserrat)�6   $��28(�61   $��Имя:��6?   $
��Left=�6N   $��1181,138,0,0q�6i   $	��Top=�6   �� 51   
   -	   5        $��NameBox��51   B   $��./Fonts/#Montserrat)�6   $��30(�63   $
��Left=�6Q   $��38��6l   $��1338,141,0,0q�6x   $
 Wrap=�6�   $	��Top=�6�   $	��282��6�   ���	#FF707070.��6�    52      ��%^   '��       �� 53      " ��6   .�54      �� 	 �@��CornerRadius$
 9��65   55      56      .��6!   57      �� 58   
   $��./Fonts/#Montserrat)�6   $��28(�61   $��Фамилия:��6?   $
��Left=�6R   $��1181,202,0,0q�6m   $	��Top=�6�   �� 59   
   -
   5        $��
SurnameBox��59   B   $��./Fonts/#Montserrat)�6   $��30(�63   $
��Left=�6T   $��38��6o   $��1338,205,0,0q�6{   $
 Wrap=�6�   $	��Top=�6�   $	��282��6�   ���	#FF707070.��6�    5:      ��%^   '��       �� 5;      " ��6   .�5<      �� 	 �@��CornerRadius$
 9��65   5=      5>      .��6!   5?      �� 5@   
   $��./Fonts/#Montserrat)�6   $��28(�61   $��Отчество:��6?   $
��Left=�6S   $��1181,266,0,0q�6n   $	��Top=�6�   �� 5A   
   -   5        $��PatronymicBox��5A   B   $��./Fonts/#Montserrat)�6   $��30(�63   $
��Left=�6W   $��38��6r   $��1338,269,0,0q�6~   $
 Wrap=�6�   $	��Top=�6�   $	��282��6�   ���	#FF707070.��6�    5B      ��%^   '��       �� 5C      " ��6   .�5D      �� 	 �@��CornerRadius$
 9��65   5E      5F      .��6!   5G      �� 5H   
   $��./Fonts/#Montserrat)�6   $��28(�61   $��Почта:��6?   $
��Left=�6P   $��1181,325,0,0q�6k   $	��Top=�6�   �� 5I   
   -   5        $��MailBox��5I   B   $��./Fonts/#Montserrat)�6   $��30(�63   $
��Left=�6Q   $��38��6l   $��1338,328,0,0q�6x   $
 Wrap=�6�   $	��Top=�6�   $	��282��6�   ���	#FF707070.��6�    5J      ��%^   '��       �� 5K      " ��6   .�5L      �� 	 �@��CornerRadius$
 9��65   5M      5N      .��6!   5O      �� 5P   
   -   5        $ ReservationButton��5P   `   ���	#FFFFBE49$��28(�6(   $"��./Fonts/#Montserrat SemiBold)�66   $ ��Забронировать��6{   $
��Left=�6�   $	��Top=�6�   $	��281��6�   $��50��6�   $��1338,384,0,0q�6�   6�   &   System.Windows.Controls.DataGrid  5Q   
   -   5          Name$ FlightsDataGrid��5Q   *      CanUserAddRows .     AutoGenerateColumns .  $��0,10,0,0q�6   ���	#FF85DCE2$��1��6�      
IsReadOnly . $��20(�6�   $��./Fonts/#Montserrat)�6�   ��6C   �� *	flights+   Columns 5R      0  *System.Windows.Controls.DataGridTextColumn  5S      ,  &System.Windows.Controls.DataGridColumn   Header$ Номер рейса��6%      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *
idflight+6W     5T      $# Свободные места��6%    6>   �� *numberseats+  5U      $ Откуда��6%    65   �� *
fromflight+  5V      $ Куда��6%    63   �� *whereflight+  5W      $% Дата отправления��6%    6?   �� *
datefrom+ �� StringFormat$ {0:dd.MM.yyyy}��  5X      $' !Время отправления��6%    6@   �� *
timefrom+  5Y      $ Дата прибытия��6%    6<   �� *	datewhere+$ {0:dd.MM.yyyy}��  5Z      $! Время прибытия��6%    6=   �� *	timewhere+  5[      $ Пересадки��6%    68   �� *Transfers.namecity+  5\      $ Цена��6%    63   �� *price+5^      5_      5`      