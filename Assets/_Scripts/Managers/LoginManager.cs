using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MyMonoBehaviour
{
    [SerializeField] private FirebaseAuth _firebaseAuth;

    // Switch form
    [Header("Switch form")]
    [SerializeField] private Transform _loginForm;
    [SerializeField] private Transform _registerForm;
    [SerializeField] private Button _btnGoToLogin;
    [SerializeField] private Button _btnGoToRegister;

    // Login
    [Header("Login")]
    [SerializeField] private TMP_InputField _ip_loginEmail;
    [SerializeField] private TMP_InputField _ip_loginPassword;
    [SerializeField] private Button _btnLogin;

    // Register
    [Header("Register")]
    [SerializeField] private TMP_InputField _ip_regEmail;
    [SerializeField] private TMP_InputField _ip_regPassword;
    [SerializeField] private TMP_InputField _ip_regComfirmPassword;
    [SerializeField] private Button _btnRegister;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSwitchFormComponent();
        LoadLoginComponent();
        LoadRegisterComponent();
    }

    private void LoadSwitchFormComponent()
    {
        if (_loginForm != null && _registerForm != null && _btnGoToLogin != null && _btnGoToRegister != null) return;
        Transform form = GameObject.Find("Form").transform;
        _loginForm = form.Find("LoginForm");
        _registerForm = form.Find("RegisterForm");
        _btnGoToLogin = _registerForm.Find("GoToLogin").GetComponent<Button>();
        _btnGoToRegister = _loginForm.Find("GoToRegister").GetComponent<Button>();
        Debug.Log(transform.name + ": LoadSwitchFormComponent", gameObject);
    }

    private void LoadLoginComponent()
    {
        if (_ip_loginEmail != null && _ip_loginPassword != null && _btnLogin != null) return;
        _ip_loginEmail = _loginForm.Find("InputEmail").GetComponent<TMP_InputField>();
        _ip_loginPassword = _loginForm.Find("InputPassword").GetComponent<TMP_InputField>();
        _btnLogin = _loginForm.Find("LoginButton").GetComponent<Button>();
        Debug.Log(transform.name + ": LoadLoginComponent", gameObject);
    }

    private void LoadRegisterComponent()
    {
        if (_ip_regEmail != null && _ip_regPassword != null && _ip_regComfirmPassword != null && _btnRegister != null) return;
        _ip_regEmail = _registerForm.Find("InputEmail").GetComponent<TMP_InputField>();
        _ip_regPassword = _registerForm.Find("InputPassword").GetComponent<TMP_InputField>();
        _ip_regComfirmPassword = _registerForm.Find("InputConfirmPassword").GetComponent<TMP_InputField>();
        _btnRegister = _registerForm.Find("RegisterButton").GetComponent<Button>();
        Debug.Log(transform.name + ": LoadRegisterComponent", gameObject);
    }

    private void Start()
    {
        _firebaseAuth = FirebaseAuth.DefaultInstance;

        AddButtonEnvent();
    }

    private void AddButtonEnvent()
    {
        _btnGoToLogin.onClick.AddListener(SwitchForm);
        _btnGoToRegister.onClick.AddListener(SwitchForm);

        _btnLogin.onClick.AddListener(LoginAccount);
        _btnRegister.onClick.AddListener(RegisterAccount);
    }

    private void SwitchForm()
    {
        _loginForm.gameObject.SetActive(!_loginForm.gameObject.activeSelf);
        _registerForm.gameObject.SetActive(!_registerForm.gameObject.activeSelf);
    }

    private void LoginAccount()
    {
        string email = _ip_loginEmail.text;
        string password = _ip_loginPassword.text;

        _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Login was cancelled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Login failed");
                return;
            }
            if (task.IsCompleted)
            {
                Debug.Log("Login successfully");
                SceneManager.LoadScene("Game");
                return;
            }
        });
    }

    private void RegisterAccount()
    {
        string email = _ip_regEmail.text;
        string password = _ip_regPassword.text;
        string confirmPassword = _ip_regComfirmPassword.text;

        if (email == "" || password == "" || confirmPassword == "")
        {
            Debug.Log("Registration information cannot be empty");
            // notice to User
            return;
        }

        if (password != confirmPassword)
        {
            Debug.Log("Confirm password does not match");
            // notice to User
            return;
        }

        _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Registration cancelled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Registration failed");
                return;
            }
            if (task.IsCompleted)
            {
                Debug.Log("Registered successfully");
                SwitchForm();
                return;
            }
        });
    }
}
