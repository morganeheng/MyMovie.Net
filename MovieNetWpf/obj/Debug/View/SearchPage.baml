   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5       MovieNetWpf!   MovieNetWpf.View.SearchPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      -   5          N�Name$  SearchPageName��5       N� Title$ 
SearchPage��5       � DataContext 5
      ��    ViewModelLocator �� Source# [   �� Path$ SearchVM�.��5      � .+�5   
   �� $
��Left=�6   $
��Wrap=�6/   $��RECHERCHER UN FILM��6C   $	��Top=�6]   $��
15,10,0,15q�6u   
�������$��40��6�   $	��675��6�   $��24(�6�    �� TextAlignment$ Center=�6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $��Titre du film : ��6C   $	��Top=�6[   $��	15,68,0,0q�6s   
�������6�   �� 5   
   -   5        $��
movieTitle��5   .   $
��Left=�6@   $��20��6[    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��430��6�   $��
110,68,0,0q�6�   
�������
���   ���6   �� *
MovieTitle+6�   �� 5   
   $��Rechercher par titre��6   $
��Left=�6Y   $	��Top=�6t   $	��120��6�   $��
550,68,0,0q�6�   
���   �
�������
���������60   �� *CommandSearchByTitle+6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $��Type du film : ��6C   $	��Top=�6Z   $��	15,93,0,0q�6r   
�������6�   �� 5   
   -   5        $��	movieType��5   -   $
��Left=�6>   $��20��6Y   $
 Wrap=�6e   $	��Top=�6y   $	��430��6�   $��
110,93,0,0q�6�   
�������
���   ���6   �� *	MovieType+6�   �� 5   
   $��Rechercher par type��6   $
��Left=�6W   $	��Top=�6r   $	��120��6�   $��
550,93,0,0q�6�   
���   �
�������
���������6/   �� *CommandSearchByType+6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $	��Top=�6^   $��
15,128,0,0q�6v   
�������$��30��6�   $	��675��6�   $��20(�6�   $ Center=�6�   ��6C   �� *	ListTitle+6�   �� 5   
   -   5         ��Name$ 	ListMovie��5      $��0q�6%   $��15,168,0,15q�6�   
���   �
���������69   �� *	ListMovie+ �� SelectedItem 6[   �� *SelectedMovie+��5      �� 5      .R�5      �� ��6    �� *Title+5      .   (System.Windows.Interactivity.Interaction	   Triggers	 5      /   )System.Windows.Interactivity.EventTrigger  5      
   	EventName$
 MouseDoubleClick��6!   .   (System.Windows.Interactivity.TriggerBase   Actions. 5      6   0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *	GoMovie+   CommandParameter 5   ,   ��  �� ElementName$ SearchPageName��5      5!      5"      5#      