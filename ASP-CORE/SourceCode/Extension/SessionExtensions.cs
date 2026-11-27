using Newtonsoft.Json;
namespace _31241021966_BuiCongDanh.Extension;
public static class SessionExtensions {
    public static void Set<T>(this ISession session,string key,T value) => session.SetString(key,JsonConvert.SerializeObject(value,new JsonSerializerSettings { ReferenceLoopHandling=ReferenceLoopHandling.Ignore }));
    public static T Get<T>(this ISession session,string key) where T:class,new() { var value=session.GetString(key); return value==null ? new T() : JsonConvert.DeserializeObject<T>(value) ?? new T(); }
}
