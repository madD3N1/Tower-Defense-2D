public class Timer 
{
    private float m_CurrentTime;

    private float m_SavedTime;

    public bool IsFinished => m_CurrentTime <= 0;

    public Timer(float startTime)
    {
        Start(startTime);
    }

    public void Start(float startTime)
    {
        m_CurrentTime = startTime;
        m_SavedTime = m_CurrentTime;
    }

    public void RemoveTime(float deltaTime)
    {
        if (m_CurrentTime <= 0) return;

        m_CurrentTime -= deltaTime;
    }

    public void Restart()
    {
        m_CurrentTime = m_SavedTime;
    }
}
