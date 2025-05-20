using GXPEngine;

public class CollisionInfo {
	public readonly Vec2 normal;
	public readonly GameObject other;
	public readonly float timeOfImpact;
    public readonly Vec2 oldPosition;

    public CollisionInfo(GameObject pOther, float pTimeOfImpact, Vec2 pNormal = new Vec2(), Vec2 pOldPosition = new Vec2()) {
		normal = pNormal;
		other = pOther;
		timeOfImpact = pTimeOfImpact;
		oldPosition = pOldPosition;
	}
}
