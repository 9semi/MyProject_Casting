using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnControl : MonoBehaviour
{
    [SerializeField]
    private Transform[] end, steepzone, rock, sand, seaweed, though;
    private float posX, posY, posZ;

    float _limitY = -1.5f;

    #region #스폰위치
    /// <summary>
    /// 각 지역 
    /// </summary>
    /// 물 높이 y 보다 낮아야하기때문에 y값은 조건 넣어줌(인스펙터에도 있음)
    /// posX, posY, posY값 설정해서 물고기위치 넣어줌
    /// <param name="fishTr">물고기 트랜스폼</param>
    /// <param name="choice">지역</param>
    /// <param name="x">지역 세로크기</param>
    /// <param name="minY">바닥 높이</param>
    /// <param name="maxY">물 높이</param>
    /// <param name="z">지역 가로크기</param>
    // 끝포인트
    public void End(Transform fishTr, int choice, float x, float minY, float maxY, float z, bool isSurface)
    {
        posX = Random.Range(end[choice].position.x - x, end[choice].position.x + x);
        //if (end[choice].position.y + minY >= _limitY && end[choice].position.y + maxY >= _limitY)
        //{
        //    posY = Random.Range(end[choice].position.y, _limitY);
        //}
        //else if (end[choice].position.y + maxY >= _limitY)
        //{
        //    posY = Random.Range(end[choice].position.y + minY, _limitY);
        //}
        //else
        //{
        //    posY = Random.Range(end[choice].position.y + minY, end[choice].position.y + maxY);
        //}

        // 해수면 기준일 경우
        if(isSurface)
        {
            // 해수면 - maxY가 스폰 바닥보다 낮으면 [ (해수면-minY) ~ 바닥 ]
            if(_limitY - maxY < end[choice].position.y)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, end[choice].position.y);
            }
            else
            {
               // Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, _limitY - maxY);
            }
        }

        // 바닥 기준일 경우
        else
        {
            // 바닥 + maxY가 해수면보다 높다면 [ (바닥+minY) ~ 해수면]
            if(end[choice].position.y + maxY > _limitY)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(end[choice].position.y + minY, _limitY);
            }
            else
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(end[choice].position.y + minY, end[choice].position.y + maxY);
            }
        }


        posZ = Random.Range(end[choice].position.z - z, end[choice].position.z + z);

        fishTr.position = new Vector3(posX, posY, posZ);
    }
    // 급심지대
    public void Steepzone(Transform fishTr, int choice, float x, float minY, float maxY, float z, bool isSurface)
    {
        posX = Random.Range(steepzone[choice].position.x - x, steepzone[choice].position.x + x);

        // 해수면 기준일 경우
        if (isSurface)
        {
            // 해수면 - maxY가 스폰 바닥보다 낮으면 [ (해수면-minY) ~ 바닥 ]
            if (_limitY - maxY < steepzone[choice].position.y)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, steepzone[choice].position.y);
            }
            else
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, _limitY - maxY);
            }
        }

        // 바닥 기준일 경우
        else
        {
            // 바닥 + maxY가 해수면보다 높다면 [ (바닥+minY) ~ 해수면]
            if (steepzone[choice].position.y + maxY > _limitY)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(steepzone[choice].position.y + minY, _limitY);
            }
            else
            {
               // Debug.Log(fishTr.name);
                posY = Random.Range(steepzone[choice].position.y + minY, steepzone[choice].position.y + maxY);
            }
        }

        posZ = Random.Range(steepzone[choice].position.z - z, steepzone[choice].position.z + z);
        fishTr.position = new Vector3(posX, posY, posZ);
    }
    // 암석지대
    public void Rock(Transform fishTr, int choice, float x, float minY, float maxY, float z, bool isSurface)
    {
        posX = Random.Range(rock[choice].position.x - x, rock[choice].position.x + x);

        // 해수면 기준일 경우
        if (isSurface)
        {
            // 해수면 - maxY가 스폰 바닥보다 낮으면 [ (해수면-minY) ~ 바닥 ]
            if (_limitY - maxY < rock[choice].position.y)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, rock[choice].position.y);
            }
            else
            {
               // Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, _limitY - maxY);
            }
        }

        // 바닥 기준일 경우
        else
        {
            // 바닥 + maxY가 해수면보다 높다면 [ (바닥+minY) ~ 해수면]
            if (rock[choice].position.y + maxY > _limitY)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(rock[choice].position.y + minY, _limitY);
            }
            else
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(rock[choice].position.y + minY, rock[choice].position.y + maxY);
            }
        }
        posZ = Random.Range(rock[choice].position.z - z, rock[choice].position.z + z);
        fishTr.position = new Vector3(posX, posY, posZ);
    }
    // 사나질
    public void Sand(Transform fishTr, int choice, float x, float minY, float maxY, float z, bool isSurface)
    {
        posX = Random.Range(sand[choice].position.x - x, sand[choice].position.x + x);

        // 해수면 기준일 경우
        if (isSurface)
        {
            // 해수면 - maxY가 스폰 바닥보다 낮으면 [ (해수면-minY) ~ 바닥 ]
            if (_limitY - maxY < sand[choice].position.y)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, sand[choice].position.y);
            }
            else
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, _limitY - maxY);
            }
        }

        // 바닥 기준일 경우
        else
        {
            // 바닥 + maxY가 해수면보다 높다면 [ (바닥+minY) ~ 해수면]
            if (sand[choice].position.y + maxY > _limitY)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(sand[choice].position.y + minY, _limitY);
            }
            else
            {
               // Debug.Log(fishTr.name);
                posY = Random.Range(sand[choice].position.y + minY, sand[choice].position.y + maxY);
            }
        }

        posZ = Random.Range(sand[choice].position.z - z, sand[choice].position.z + z);

        fishTr.position = new Vector3(posX, posY, posZ);
    }
    // 해조류
    public void Seaweed(Transform fishTr, int choice, float x, float minY, float maxY, float z, bool isSurface)
    {
        posX = Random.Range(seaweed[choice].position.x - x, seaweed[choice].position.x + x);

        // 해수면 기준일 경우
        if (isSurface)
        {
            // 해수면 - maxY가 스폰 바닥보다 낮으면 [ (해수면-minY) ~ 바닥 ]
            if (_limitY - maxY < seaweed[choice].position.y)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, seaweed[choice].position.y);
            }
            else
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, _limitY - maxY);
            }
        }

        // 바닥 기준일 경우
        else
        {
            // 바닥 + maxY가 해수면보다 높다면 [ (바닥+minY) ~ 해수면]
            if (seaweed[choice].position.y + maxY > _limitY)
            {
               // Debug.Log(fishTr.name);
                posY = Random.Range(seaweed[choice].position.y + minY, _limitY);
            }
            else
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(seaweed[choice].position.y + minY, seaweed[choice].position.y + maxY);
            }
        }

        posZ = Random.Range(seaweed[choice].position.z - z, seaweed[choice].position.z + z);
        fishTr.position = new Vector3(posX, posY, posZ);
    }
    // 물골
    public void Though(Transform fishTr, int choice, float x, float minY, float maxY, float z, bool isSurface)
    {
        posX = Random.Range(though[choice].position.x - x, though[choice].position.x + x);

        // 해수면 기준일 경우
        if (isSurface)
        {
            // 해수면 - maxY가 스폰 바닥보다 낮으면 [ (해수면-minY) ~ 바닥 ]
            if (_limitY - maxY < though[choice].position.y)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, though[choice].position.y);
            }
            else
            {
               // Debug.Log(fishTr.name);
                posY = Random.Range(_limitY - minY, _limitY - maxY);
            }
        }

        // 바닥 기준일 경우
        else
        {
            // 바닥 + maxY가 해수면보다 높다면 [ (바닥+minY) ~ 해수면]
            if (though[choice].position.y + maxY > _limitY)
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(though[choice].position.y + minY, _limitY);
            }
            else
            {
                //Debug.Log(fishTr.name);
                posY = Random.Range(though[choice].position.y + minY, though[choice].position.y + maxY);
            }
        }
        posZ = Random.Range(though[choice].position.z - z, though[choice].position.z + z);
        fishTr.position = new Vector3(posX, posY, posZ);
    }
    #endregion

    #region #물고기 움직임
    /// <summary>
    /// 물고기 움직임 제어 
    /// </summary>
    /// 물고기의 다음위치가 지역을 벗어나면 반대방향으로 돌려줌
    /// <param name="fishTr">물고기 트랜스폼</param>
    /// <param name="choice">지역</param>
    /// <param name="dirX">x 방향</param>
    /// <param name="dirY">y 방향</param>
    /// <param name="dirZ">z 방향</param>
    /// <param name="x">지역 세로크기</param>
    /// <param name="minY">바닥 높이</param>
    /// <param name="maxY">물 높이</param>
    /// <param name="z">지역 가로크기</param>
    // 끝포인트
    public void EndMove(Transform fishTr, int choice, float dirX, float dirY, float dirZ, float x, float minY, float maxY, float z)
    {
        fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y + dirY, fishTr.position.z + dirZ);

        if (fishTr.position.x <= end[choice].position.x - x || fishTr.position.x >= end[choice].position.x + x)
        {
            //dirX *= -1;
            fishTr.position = new Vector3(fishTr.position.x - dirX, fishTr.position.y, fishTr.position.z);
        }

        //if (end[choice].position.y + minY >= -3 && end[choice].position.y + maxY >= -3)
        //{
        //    if (fishTr.position.y <= end[choice].position.y ||fishTr.position.y >= -3)
        //    {
        //        dirY *= -1;
        //        fishTr.position = new Vector3(fishTr.position.x, fishTr.position.y + dirY, fishTr.position.z);
        //    }
        //}
        //else if (end[choice].position.y + maxY >= _limitY)
        //{
        //    if (fishTr.position.y <= end[choice].position.y + minY ||fishTr.position.y >= -3)
        //    {
        //        dirY *= -1;
        //        fishTr.position = new Vector3(fishTr.position.x, fishTr.position.y + dirY, fishTr.position.z);
        //    }
        //}
        //else if (fishTr.position.y <= end[choice].position.y + minY || fishTr.position.y >= end[choice].position.y + maxY)
        //{
        //    dirY *= -1;
        //    fishTr.position = new Vector3(fishTr.position.x, fishTr.position.y + dirY, fishTr.position.z);
        //}

        if(fishTr.position.y <= end[choice].position.y)
        {
            fishTr.position = new Vector3(fishTr.position.x, end[choice].position.y + minY, fishTr.position.z);
        }
        else if(fishTr.position.y >= _limitY)
        {
            fishTr.position = new Vector3(fishTr.position.x, _limitY - dirY, fishTr.position.z);
        }

        if (fishTr.position.z <= end[choice].position.z - z ||
            fishTr.position.z >= end[choice].position.z + z)
        {
            //dirZ *= -1;
            fishTr.position = new Vector3(fishTr.position.x, fishTr.position.y, fishTr.position.z - dirZ);
        }
    }
    // 급심지대
    public void SteepzoneMove(Transform fishTr, int choice, float dirX, float dirY, float dirZ, float x, float minY, float maxY, float z)
    {
        fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y + dirY, fishTr.position.z + dirZ);
        if (fishTr.position.x <= steepzone[choice].position.x - x ||
            fishTr.position.x >= steepzone[choice].position.x + x)
        {
            dirX *= -1;
            fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y, fishTr.position.z);
        }

        if (fishTr.position.y <= steepzone[choice].position.y)
        {
            fishTr.position = new Vector3(fishTr.position.x, steepzone[choice].position.y + minY, fishTr.position.z);
        }
        else if (fishTr.position.y >= _limitY)
        {
            fishTr.position = new Vector3(fishTr.position.x, _limitY - dirY, fishTr.position.z);
        }

        if (fishTr.position.z <= steepzone[choice].position.z - z ||
            fishTr.position.z >= steepzone[choice].position.z + z)
        {
            dirZ *= -1;
            fishTr.position = new Vector3(fishTr.position.x, fishTr.position.y, fishTr.position.z + dirZ);            
        }
    }
    // 암석지대
    public void RockMove(Transform fishTr, int choice, float dirX, float dirY, float dirZ, float x, float minY, float maxY, float z)
    {
        fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y + dirY, fishTr.position.z + dirZ);
        if (fishTr.position.x <= rock[choice].position.x - x ||
            fishTr.position.x >= rock[choice].position.x + x)
        {
            dirX *= -1;
            fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y, fishTr.position.z);
        }

        if (fishTr.position.y <= rock[choice].position.y)
        {
            fishTr.position = new Vector3(fishTr.position.x, rock[choice].position.y + minY, fishTr.position.z);
        }
        else if (fishTr.position.y >= _limitY)
        {
            fishTr.position = new Vector3(fishTr.position.x, _limitY - dirY, fishTr.position.z);
        }

        if (fishTr.position.z <= rock[choice].position.z - z ||
            fishTr.position.z >= rock[choice].position.z + z)
        {
            dirZ *= -1;
            fishTr.position = new Vector3(fishTr.position.x, fishTr.position.y, fishTr.position.z + dirZ);
        }
    }
    // 사나질
    public void SandMove(Transform fishTr, int choice, float dirX, float dirY, float dirZ, float x, float minY, float maxY, float z)
    {
        fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y + dirY, fishTr.position.z + dirZ);
        if (fishTr.position.x <= sand[choice].position.x - x ||
            fishTr.position.x >= sand[choice].position.x + x)
        {
            dirX *= -1;
            fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y, fishTr.position.z);
        }
        if (fishTr.position.y <= sand[choice].position.y)
        {
            fishTr.position = new Vector3(fishTr.position.x, sand[choice].position.y + minY, fishTr.position.z);
        }
        else if (fishTr.position.y >= _limitY)
        {
            fishTr.position = new Vector3(fishTr.position.x, _limitY - dirY, fishTr.position.z);
        }
        if (fishTr.position.z <= sand[choice].position.z - z ||
            fishTr.position.z >= sand[choice].position.z + z)
        {
            dirZ *= -1;
            fishTr.position = new Vector3(fishTr.position.x, fishTr.position.y, fishTr.position.z + dirZ);
        }
    }
    // 해조류
    public void SeaweedMove(Transform fishTr, int choice, float dirX, float dirY, float dirZ, float x, float minY, float maxY, float z)
    {
        fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y + dirY, fishTr.position.z + dirZ);

        // x
        if (fishTr.position.x <= seaweed[choice].position.x - x || fishTr.position.x >= seaweed[choice].position.x + x)
        {
            dirX *= -1;
            fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y, fishTr.position.z);
        }

        // y
        if (fishTr.position.y <= seaweed[choice].position.y)
        {
            fishTr.position = new Vector3(fishTr.position.x, seaweed[choice].position.y + minY, fishTr.position.z);
        }
        else if (fishTr.position.y >= _limitY)
        {
            fishTr.position = new Vector3(fishTr.position.x, _limitY - dirY, fishTr.position.z);
        }

        // z
        if (fishTr.position.z <= seaweed[choice].position.z - z ||
            fishTr.position.z >= seaweed[choice].position.z + z)
        {
            dirZ *= -1;
            fishTr.position = new Vector3(fishTr.position.x, fishTr.position.y, fishTr.position.z + dirZ);
        }
    }
    // 물골
    public void ThoughMove(Transform fishTr, int choice, float dirX, float dirY, float dirZ, float x, float minY, float maxY, float z)
    {
        fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y + dirY, fishTr.position.z + dirZ);
        if (fishTr.position.x <= though[choice].position.x - x ||
            fishTr.position.x >= though[choice].position.x + x)
        {
            dirX *= -1;
            fishTr.position = new Vector3(fishTr.position.x + dirX, fishTr.position.y, fishTr.position.z);
        }
        if (fishTr.position.y <= though[choice].position.y)
        {
            fishTr.position = new Vector3(fishTr.position.x, though[choice].position.y + minY, fishTr.position.z);
        }
        else if (fishTr.position.y >= _limitY)
        {
            fishTr.position = new Vector3(fishTr.position.x, _limitY - dirY, fishTr.position.z);
        }
        if (fishTr.position.z <= though[choice].position.z - z ||
            fishTr.position.z >= though[choice].position.z + z)
        {
            dirZ *= -1;
            fishTr.position = new Vector3(fishTr.position.x, fishTr.position.y, fishTr.position.z + dirZ);
        }
    }
    #endregion
}
