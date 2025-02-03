 
using UnityEngine;

public class MeshHead : MonoBehaviour
{
    public SkinnedMeshRenderer face;
    public SkinnedMeshRenderer hair;


    private int _currentEmotion;
    private float _nextDelay;
    private float _currentDelay;
    private float _currentWeight = 0;
    private float _animationShowTime;




    private void Update()
    {
        if (_nextDelay <= 0) // start emotion
        {
            _currentEmotion = UnityEngine.Random.Range(1, 4);

            _animationShowTime = 2;
            _nextDelay = UnityEngine.Random.Range(4, 8);
        }
        else
        {
            _nextDelay -= Time.deltaTime;
            _animationShowTime -= Time.deltaTime;

            if (_animationShowTime > 0)
            {
                if(_currentWeight > 100)
                {
                    _currentWeight = 100;
                }
                else
                {
                    _currentWeight += Time.deltaTime * 210;
                }
            }
            else
            {
                if(_currentWeight <= 0)
                {
                    _currentWeight = 0;
                }
                else
                { 
                    _currentWeight -= Time.deltaTime * 210;
                }
            }


        }

        face.SetBlendShapeWeight(_currentEmotion, _currentWeight);
    }


     

}