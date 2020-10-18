namespace Fwk.Math
{
	public static class MapMath
	{
		// 点（緯度経度）間の距離、単位m
		public static double CalculateDistanceHubeny(Vector2d location0, Vector2d location1)
		{
			const double WGS84_A = 6378137.000;
			const double WGS84_E2 = 0.00669437999019758;
			const double WGS84_MNUM = 6335439.32729246;

			// var my = ( location0.y + location1.y ) * Mathd.PI / 180.0 * 0.5;    // 緯度平均
			// var dy = ( location0.y - location1.y ) * Mathd.PI / 180.0;          // 緯度差
			// var dx = ( location0.x - location1.x ) * Mathd.PI / 180.0;          // 経度差
			var my = (location0.x + location1.x) * Mathd.PI / 180.0 * 0.5;    // 緯度平均
			var dy = (location0.x - location1.x) * Mathd.PI / 180.0;          // 緯度差
			var dx = (location0.y - location1.y) * Mathd.PI / 180.0;          // 経度差

			var sin = Mathd.Sin(my);
			var w = Mathd.Sqrt(1.0 - WGS84_E2 * sin * sin);
			var m = WGS84_MNUM / (w * w * w);
			var n = WGS84_A / w;

			var dym = dy * m;
			var dxncos = dx * n * Mathd.Cos(my);

			return Mathd.Sqrt(dym * dym + dxncos * dxncos);
		}
	}
} // Fwk.Math
