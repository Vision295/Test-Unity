#include <stdio.h>


void Ex1(int arr[], int size, int i, int j)
{
    printf("%d \n", size);

    for (int k = 0; k < size; k++)
        printf("%d ", arr[k]);
    printf("\n");

    int a;
    a = arr[i];
    arr[i] = arr[j];
    arr[j] = a;

    for (int p = 0; p < size; p++)
        printf("%d ", arr[p]);
}

int Ex2(char charlst[], int size)
{
    int total = 0;
    for(int i = 0; i < size; i++)
    {
        if(65 <= (int)charlst[i] <= 90 || (int)charlst[i] == 32)
        {
            total++;
        }
    }
    return total;
}

void sort_func(int size , int *tab)
{
    int i , j , aux;
    for (i = 0; i < size - 1; i++)
    {
        for (j = i + 1; j < size; j ++)
        {
            if( tab[i] >= tab [j])
            {
                aux = tab[i];
                tab[i] = tab[j];
                tab [j] = aux;
            }
        }
    }
}


int main(void)
{
    // EXERCISE 1
    int l[10] = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    Ex1(l, sizeof(l) / sizeof(l[0]), 0, 1);
    printf("\n");

    // EXERCISE 2
    char charlst[5] = "Aa zZ";
    printf("%d", Ex2(charlst, sizeof(charlst) / sizeof(charlst[0])));
    printf("\n");

    // EXERCISE 3
    int operation = 1;
    int matrix1[3][3] = {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
    int matrix2[3][3] = {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
    int matrix[3][3] = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}};

    switch (operation)
    {
        case 1:
            for (int i = 0; i < sizeof(matrix1) / sizeof(matrix1[0]); i++)
            {
                for (int j = 0; j < sizeof(matrix1[i]) / sizeof(matrix1[i][0]); j++)
                {
                    matrix[i][j] = matrix1[i][j] + matrix2[i][j];
                    printf("%d ", matrix[i][j]);
                }
            }
            break;
        case 2:
            for (int i = 0; i < sizeof(matrix1) / sizeof(matrix1[0]); i++)
            {
                for (int j = 0; j < sizeof(matrix1[i]) / sizeof(matrix1[i][0]); j++)
                {
                    matrix[i][j] = matrix1[i][j] * matrix2[i][j];
                    printf("%d ", matrix[i][j]);
                }
            }
        
        case 3:
            for (int i = 0; i < sizeof(matrix1) / sizeof(matrix1[0]); i++)
            {
                for (int j = 0; j < sizeof(matrix1[i]) / sizeof(matrix1[i][0]); j++)
                {
                    matrix[i][j] = matrix1[i][j] - matrix2[i][j];
                    printf("%d ", matrix[i][j]);
                }
            }
            break;
    }
    printf("\n\n");

    // EXERCISE 5
    int ar[6] = {1, 2, 3, 4, 5};
    sort_func(sizeof(ar) / sizeof(ar[0]), ar);

    int insertion[3] = {1, 999, 88};    
    int sorted_array[5] = {1, 2, 8, 10, 100};
    int size1 = sizeof(insertion) / sizeof(insertion[0]);
    int size2 = sizeof(sorted_array) / sizeof(sorted_array[0]);

    sort_func(size1, insertion);
    
    int final_list[100] = {0, 0, 0, 0, 0, 0, 0, 0};

    
    int j = 0, k = 0;
    printf("Here we have inserted insertion into sorted_array giving us : ");
    for (int i = 0; i < size1 + size2; i++)
    {
        if (j < size1 && k < size2)
        {
            if (insertion[j] <= sorted_array[k])
            {
                final_list[i] = insertion[j];
                j++;
            } else 
            {
                final_list[i] = sorted_array[k];
                k++;
            }
        } else if (j < size1)
        {
            final_list[i] = insertion[j];
            j++;
        }
        else if(k < size2)
        {
            final_list[i] = sorted_array[k];
            k++;
        }
            
        printf("%d ", final_list[i]);
    }

    return 0;
}