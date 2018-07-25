using UnityEngine;

public class CellularAutomaton{
    public byte[,] cells;
    public float[,] heat_map;
    public int sizeX;
    public int sizeY;

    public CellularAutomaton(int _sizeX,int _sizeY, float fill_percentage){
        sizeX = _sizeX;
        sizeY = _sizeY;
        cells = new byte[sizeX,sizeY];
        heat_map = new float[sizeX,sizeY];
        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++)
                cells[x,y] = Random.Range(0, 100) < fill_percentage ? (byte)1 : (byte)0;
    }

    public int GetCellValue(int x,int y){
        return cells[x,y];
    }
    public float GetHeatValue(int x, int y){
        return heat_map[x, y];
    }

    private int GetNeighbor(int x, int y){
        int neighbor = 0;
        for (int j = -1; j < 2; j++)
            for (int k = -1; k < 2; k++)
                if (!(j == 0 && k == 0)){
                    int _x = (x + j + sizeX) % sizeX;
                    int _y = (y + k + sizeY) % sizeY;
                    neighbor += cells[_x,_y];
                }

        return neighbor;
    }

    public void Step(){
        byte[,] temp_cells = new byte[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
            for (int y = 0; y < sizeY; y++){
                temp_cells[x, y] = GetValueFromRules(x, y);
                if (cells[x, y] != temp_cells[x, y])
                    heat_map[x, y] += 0.01f;
            }
        
        cells = temp_cells;
    }

    private byte GetValueFromRules(int x, int y){
        byte status = cells[x, y];
        int n = GetNeighbor(x, y);

        if (status == 0 && n == 3)
            return 1;
        else if (status == 1 && (n < 2 || n > 3))
            return 0;
        else
            return status;

    }

}
