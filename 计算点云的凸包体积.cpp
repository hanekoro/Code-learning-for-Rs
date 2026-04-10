#include <iostream>
#include <fstream>
#include <vector>
#include <cmath>
#include <limits>
using namespace std;



struct Point {
    double x, y, z;
};

struct Face {
    int a, b, c;  // indices of points forming a face
};

// 计算向量叉乘
Point crossProduct(const Point& a, const Point& b) {
    return {
        a.y * b.z - a.z * b.y,
        a.z * b.x - a.x * b.z,
        a.x * b.y - a.y * b.x
    };
}

// 计算向量差
Point subtract(const Point& a, const Point& b) {
    return { a.x - b.x, a.y - b.y, a.z - b.z };
}

// 计算点到平面的有向距离
double pointPlaneDistance(const Point& p, const Point& a, const Point& b, const Point& c) {
    Point n = crossProduct(subtract(b, a), subtract(c, a)); // 平面法向量
    double area2 = sqrt(n.x * n.x + n.y * n.y + n.z * n.z);
    if (area2 == 0) return 0;

    double d = (n.x * (p.x - a.x) + n.y * (p.y - a.y) + n.z * (p.z - a.z)) / area2;
    return d;
}

// QuickHull（递归寻找最远点并构建面）
void quickHull(const vector<Point>& pts, int a, int b, int c, vector<int>& out, const vector<int>& candidates) {
    double maxDist = 0;
    int farthest = -1;

    Point A = pts[a], B = pts[b], C = pts[c];

    for (int idx : candidates) {
        double d = fabs(pointPlaneDistance(pts[idx], A, B, C));
        if (d > maxDist) {
            maxDist = d;
            farthest = idx;
        }
    }

    if (farthest == -1) {
        out.push_back(a);
        out.push_back(b);
        out.push_back(c);
        return;
    }

    // 分裂候选点集合
    vector<int> left1, left2, left3;
    for (int idx : candidates) {
        if (idx == farthest) continue;

        if (pointPlaneDistance(pts[idx], A, B, pts[farthest]) > 0)
            left1.push_back(idx);

        if (pointPlaneDistance(pts[idx], B, C, pts[farthest]) > 0)
            left2.push_back(idx);

        if (pointPlaneDistance(pts[idx], C, A, pts[farthest]) > 0)
            left3.push_back(idx);
    }

    quickHull(pts, a, b, farthest, out, left1);
    quickHull(pts, b, c, farthest, out, left2);
    quickHull(pts, c, a, farthest, out, left3);
}

// 四面体体积公式：|dot(a, cross(b, c))| / 6
double tetraVolume(const Point& o, const Point& a, const Point& b, const Point& c) {
    Point A = subtract(a, o);
    Point B = subtract(b, o);
    Point C = subtract(c, o);

    Point N = crossProduct(B, C);
    double vol = fabs(A.x * N.x + A.y * N.y + A.z * N.z) / 6.0;
    return vol;
}

int main() {
    // 读取点云
    ifstream file("D:/data/point_cloud.txt");
    if (!file.is_open()) {
        cout << "无法打开 point_cloud.txt\n";
        return 1;
    }

    vector<Point> pts;
    double x, y, z;
    while (file >> x >> y >> z) {
        pts.push_back({ x, y, z });
    }

    cout << "读取点数: " << pts.size() << endl;

    if (pts.size() < 4) {
        cout << "点数量不足以构成凸包\n";
        return 1;
    }

    // 找到范围最大的 3 个点作为初始面
    int a = 0, b = 1, c = 2;
    vector<int> candidates;
    for (int i = 0; i < pts.size(); i++) candidates.push_back(i);

    // QuickHull 递归构建凸包三角面
    vector<int> hullIndices;
    quickHull(pts, a, b, c, hullIndices, candidates);

    // 计算体积（以点0为原点累加多个四面体）
    double totalVolume = 0;
    for (int i = 0; i + 2 < hullIndices.size(); i += 3) {
        int i1 = hullIndices[i];
        int i2 = hullIndices[i + 1];
        int i3 = hullIndices[i + 2];
        totalVolume += tetraVolume(pts[0], pts[i1], pts[i2], pts[i3]);
    }

    cout << "凸包体积: " << totalVolume << endl;

    return 0;
}
